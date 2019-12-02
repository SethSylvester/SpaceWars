using System;
using System.Collections.Generic;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class AABB : Actor
    {
        public float Width { get; set; } = 1;
        public float Height { get; set; } = 1;

        //Returns the Y coordinate at the top of the box
        public float Top
        {
            get { return YAbsolute - Height / 2; }
        }

        //Returns the Y coordinate at the top of the box
        public float Bottom
        {
            get { return YAbsolute + Height / 2; }
        }

        //Returns the X coordinate at the top of the box
        public float Left
        {
            get { return XAbsolute - Width / 2; }
        }

        //Returns the X coordinate at the top of the box
        public float Right
        {
            get { return XAbsolute + Width / 2; }
        }

        public bool DetectCollision(AABB other)
        {
            // test for not overlapped as it exits faster
            return !(Bottom < other.Top || Right < other.Left ||
            Top > other.Bottom || Left > other.Right);
        }

        public bool DetectCollision(Vector3 point)
        {
            // test for not overlapped as it exits faster
            return !(point.y < Top || point.x < Left ||
            point.y > Bottom || point.x > Right);
        }

        //Draw the bounding box to the screen
        public override void Draw()
        {
            Raylib.Rectangle rec = new Raylib.Rectangle(Left, Top, Width, Height);
            Raylib.Raylib.DrawRectangleLinesEx(rec, 5, Raylib.Color.RED);
            base.Draw();
        }

        private Vector3 _min = new Vector3(
            float.NegativeInfinity,
            float.NegativeInfinity,
            float.NegativeInfinity);

        private Vector3 _max = new Vector3(
            float.PositiveInfinity,
            float.PositiveInfinity,
            float.PositiveInfinity);

        //Creates an AABB of the specifed size
        public AABB(float width, float height)
        {
            Width = width;
            Height = height;
        }
        public AABB(float width, float height, float x, float y)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        public void Move(Vector3 point)
        {
            Vector3 extents = Extents();
            _min = point - extents;
            _max = point + extents;
        }

        public Vector3 Center()
        {
            return (_min + _max) * 0.5f;
        }
        public Vector3 Extents()
        {
            return new Vector3(Math.Abs(_max.x - _min.x) * 0.5f,
            Math.Abs(_max.y - _min.y) * 0.5f,
            Math.Abs(_max.z - _min.z) * 0.5f);
        }

        public List<Vector3> Corners()
        {
            // ignoring z axis for 2D
            List<Vector3> corners = new List<Vector3>(4);
            corners[0] = _min;
            corners[1] = new Vector3(_min.x, _max.y, _min.z);
            corners[2] = _max;
            corners[3] = new Vector3(_max.x, _min.y, _min.z);
            return corners;
        }

        public void Fit(List<Vector3> points)
        {
            // invalidate the extents
            _min = new Vector3(float.PositiveInfinity,
            float.PositiveInfinity,
           float.PositiveInfinity);
            _max = new Vector3(float.NegativeInfinity,
            float.NegativeInfinity,
           float.NegativeInfinity);
            // find min and max of the points
            foreach (Vector3 p in points)
            {
                _min = Vector3.Min(_min, p);
                _max = Vector3.Max(_max, p);
            }
        }

        public void Fit(Vector3[] points)
        {
            // invalidate the extents
            _min = new Vector3(float.PositiveInfinity,
            float.PositiveInfinity,
           float.PositiveInfinity);
            _max = new Vector3(float.NegativeInfinity,
            float.NegativeInfinity,
           float.NegativeInfinity);
            // find min and max of the points
            foreach (Vector3 p in points)
            {
                _min = Vector3.Min(_min, p);
                _max = Vector3.Max(_max, p);
            }
        }

        public bool Overlaps(Vector3 p)
        {
            // test for not overlapped as it exits faster
            return !(p.x < _min.x || p.y < _min.y ||
            p.x > _max.x || p.y > _max.y);
        }

        public bool Overlaps(AABB other)
        {
            // test for not overlapped as it exits faster
            return !(_max.x < other._min.x || _max.y < other._min.y ||
            _min.x > other._max.x || _min.y > other._max.y);
        }

    }
}