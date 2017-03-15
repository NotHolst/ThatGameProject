using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TDG.Pathfinding {
    public class Pathfinder {

        int[,] map;

        public HashSet<Node> openList { get; private set; }
        public List<Node> closedList { get; private set; }

        public List<Node> path { get; private set; }

        Stopwatch stopwatch = new Stopwatch();

        public Pathfinder(int [,] map) {
            this.map = map;
        }

        public void FindPath(Node start, Node goal) {
            
            stopwatch.Start();

            openList = new HashSet<Node>();
            closedList = new List<Node>();

            openList.Add(start);
            while (openList.Count > 0) {

                Node current = GetLowestFCost();

                if (current.Equals(goal)) {
                    ReconstructPath(current);
                    return;
                }

                openList.Remove(current);
                closedList.Add(current);

                foreach (Node n in Neighbors(current)) {
                    if (closedList.Contains(n)) continue;
                    n.g_score = current.g_score + 1;
                    n.h_score = ManhattanDistance(n, goal);

                    if (!openList.Contains(n)) {
                        openList.Add(n);
                    }

                    n.parent = current;

                }

            }
            
        }

        private void ReconstructPath(Node nGoal) {
            stopwatch.Stop();
            Console.WriteLine("{0} nodes in list", openList.Count);
            Console.WriteLine("Pathfinding ended in: {0}ms", stopwatch.ElapsedMilliseconds);

            path = new List<Node>();

            Node n = nGoal;

            while(n.parent != null) {
                path.Add(n);
                n = n.parent;
            }
            path.Reverse();


        }

        private float ManhattanDistance(Node a, Node b) {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

        }

        private Node GetLowestFCost() {
            Node lowest = null;
            foreach (Node n in openList) {
                if (lowest == null || n.f_score < lowest.f_score) {
                    lowest = n;
                }
            }

            return lowest;

        }

        private List<Node> Neighbors(Node n) {

            List<Node> neighbors = new List<Node>();

            for (int dx = -1; dx <= 1; dx++) {
                for (int dy = -1; dy <= 1; dy++) {
                    if ((dx == 0 && dy == 0) || (dx != 0 && dy!= 0))
                        continue;
                    if (n.x + dx < 0 || n.y + dy < 0
                        || n.x + dx >= map.GetLength(0) || n.y + dy >= map.GetLength(1))
                        continue;
                    if (map[n.x + dx, n.y + dy] == 1) continue;

                    Node neighbor = new Node(n.x + dx, n.y + dy);

                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }
    }
}
