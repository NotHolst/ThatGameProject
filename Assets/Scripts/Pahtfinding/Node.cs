using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDG.Pathfinding {
    public class Node {
        public int x;
        public int y;
        public Node parent;
        public float g_score;
        public float h_score;
        public float f_score { get { return this.g_score + this.h_score; } }

        public Node(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(Object other) {
            if (!(other is Node)) return false;
            Node n = (Node)other;
            return this.x == n.x && this.y == n.y;
        }

        public override int GetHashCode() {
            int hash = 13;
            hash = (hash * 7) + x.GetHashCode();
            hash = (hash * 7) + y.GetHashCode();
            return hash;
        }
    }
}
