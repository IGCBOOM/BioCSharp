using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BioCSharp.Core.Util
{
    public class SingleLinkageClusterer
    {
        public class LinkedPair
        {

            private int _first;
            private int _second;
            private double _closestDistance;

            public LinkedPair(int first, int second, double minDistance)
            {

                this._first = first;
                this._second = second;
                this._closestDistance = minDistance;

            }

            public int GetFirst()
            {
                return _first;
            }

            public int GetSecond()
            {
                return _second;
            }

            public double GetClosestDistance()
            {
                return _closestDistance;
            }

            public override string ToString()
            {

                string closestDistanceStr = null;
                if (_closestDistance == Double.MaxValue)
                {
                    closestDistanceStr = "inf";
                }
                else
                {
                    closestDistanceStr = _closestDistance.ToString();
                }

                return "[" + _first + "," + _second + "," + closestDistanceStr + "]";

            }
        }

        private double[][] _matrix;
        private bool _isScoreMatrix;
        private int _numItems;
        private LinkedPair[] _dendrogram;
        private List<int> _indicesToCheck;

        public SingleLinkageClusterer(double[][] matrix, bool isScoreMatrix)
        {

            this._matrix = matrix;
            this._isScoreMatrix = isScoreMatrix;

            if (_matrix.Length != matrix[0].Length)
            {
                throw new ArgumentException("Distance matrix for clustering must be a square matrix");
            }

            this._numItems = matrix.Length;

        }

        public LinkedPair[] GetDendrogram()
        {

            if (_dendrogram == null)
            {
                
            }

            return _dendrogram;
        }

        private void ClusterIt()
        {

            _dendrogram = new LinkedPair[_numItems - 1];

            for (var m = 0; m < _numItems; m++)
            {
                
                UpdateIndicesToCheck(m);
                var pair = GetClosestPair();
                Merge(pair);
                _dendrogram[m] = pair;

            }

        }

        private void Merge(LinkedPair closestPair)
        {

            var first = closestPair.GetFirst();
            var second = closestPair.GetSecond();

            for (var other = 0; other < _numItems; other++)
            {
                _matrix[Math.Min(first, other)][Math.Max(first, other)] = Link(GetDistance(first, other), GetDistance(second, other));
            }

        }

        private double Link(double d1, double d2)
        {
            return _isScoreMatrix ? Math.Max(d1, d2) : Math.Min(d1, d2);
        }

        private double GetDistance(int first, int second)
        {
            return _matrix[Math.Min(first, second)][Math.Max(first, second)];
        }

        private void UpdateIndicesToCheck(int m)
        {

            if (_indicesToCheck == null)
            {
                
                _indicesToCheck = new List<int>(_numItems);

                for (int i = 0; i < _numItems; i++)
                {
                    _indicesToCheck.Add(i);
                }

            }

            if (m == 0)
            {
                return;
            }

            _indicesToCheck.Remove(_dendrogram[m-1].GetFirst());

        }

        private LinkedPair GetClosestPair()
        {

            LinkedPair closestPair = null;

            if (_isScoreMatrix)
            {

                var max = 0.0;

                foreach (var i in _indicesToCheck)
                {

                    foreach (var j in _indicesToCheck)
                    {

                        if (j <= i)
                            continue;

                        if (!(_matrix[i][j] >= max)) continue;
                        max = _matrix[i][j];
                        closestPair = new LinkedPair(i, j, max);

                    }

                }

            }
            else
            {
                
                var min = double.MaxValue;

                foreach (var i in _indicesToCheck)
                {

                    foreach (var j in _indicesToCheck)
                    {

                        if (j <= i)
                            continue;

                        if (!(_matrix[i][j] <= min)) continue;
                        min = _matrix[i][j];
                        closestPair = new LinkedPair(i, j, min);

                    }

                }

            }

            return closestPair;

        }

        public SortedDictionary<int, HashSet<int>> GetClusters(double cutoff)
        {

            if (_dendrogram == null)
            {
                ClusterIt();
            }

            var clusters = new SortedDictionary<int, HashSet<int>>();

            var clusterId = 1;

            for (var i = 0; i < _numItems - 1; i++)
            {

                if (IsWithinCutoff(i, cutoff))
                {

                    var firstClusterId = -1;
                    var secondClusterId = -1;
                    foreach (var cId in clusters.Keys)
                    {

                        HashSet<int> members = clusters[cId];

                        if (members.Contains(_dendrogram[i].GetFirst()))
                        {
                            firstClusterId = cId;
                        }

                        if (members.Contains(_dendrogram[i].GetSecond()))
                        {
                            secondClusterId = cId;
                        }

                    }

                    if (firstClusterId == -1 && secondClusterId == -1)
                    {
                        
                        HashSet<int> members = new HashSet<int>();
                        members.Add(_dendrogram[i].GetFirst());
                        members.Add(_dendrogram[i].GetSecond());
                        clusters[clusterId] = members;
                        clusterId++;

                    }
                    else if (firstClusterId != -1 && secondClusterId == -1)
                    {
                        clusters[firstClusterId].Add(_dendrogram[i].GetSecond());
                    }
                    else if (firstClusterId == -1 && secondClusterId != -1)
                    {
                        clusters[secondClusterId].Add(_dendrogram[i].GetFirst());
                    }
                    else
                    {

                        HashSet<int> firstCluster = clusters[firstClusterId];
                        HashSet<int> secondCluster = clusters[secondClusterId];

                        if (firstCluster.Count < secondCluster.Count)
                        {

                            foreach (var member in firstCluster)
                            {
                                secondCluster.Add(member);
                            }

                            clusters.Remove(firstClusterId);

                        }
                        else
                        {

                            foreach (var member in secondCluster)
                            {
                                firstCluster.Add(member);
                            }

                            clusters.Remove(secondClusterId);

                        }

                    }

                }
                else
                {
                    //Not Within cutoff
                }

            }

            SortedDictionary<int, HashSet<int>> finalClusters = new SortedDictionary<int, HashSet<int>>();
            int newClusterId = 1;
            foreach (var oldClusterId in clusters.Keys)
            {
                
                finalClusters[newClusterId] = clusters[oldClusterId];
                newClusterId++;

            }

            for (int i = 0; i < _numItems; i++)
            {

                bool isAlreadyClustered = false;
                foreach (var cluster in finalClusters.Values)
                {

                    if (cluster.Contains(i))
                    {

                        isAlreadyClustered = true;
                        break;

                    }

                }

                if (!isAlreadyClustered)
                {
                    
                    HashSet<int> members = new HashSet<int>();
                    members.Add(i);
                    finalClusters[newClusterId] = members;
                    newClusterId++;

                }

            }

            return finalClusters;

        }

        private bool IsWithinCutoff(int i, double cutoff)
        {

            if (_isScoreMatrix)
            {
                return _dendrogram[i].GetClosestDistance() > cutoff;
            }
            else
            {
                return _dendrogram[i].GetClosestDistance() < cutoff;
            }

        }

        //TODO Add ToString Methods

    }
}
