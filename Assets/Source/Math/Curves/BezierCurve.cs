﻿using System;
using UnityEngine;

namespace Zetta.Math.Curves
{
    public class BezierCurve : ICurveProvider
    {
        private static Lazy<BezierCurve> ease = new Lazy<BezierCurve>(
            () => new BezierCurve(
                new Vector2(0.25f, 0.1f),
                new Vector2(0.25f, 0.1f)));

        private static Lazy<BezierCurve> linear = new Lazy<BezierCurve>(
            () => new BezierCurve(
                new Vector2(0f, 0f),
                new Vector2(1f, 1f)));

        private static Lazy<BezierCurve> easeIn = new Lazy<BezierCurve>(
            () => new BezierCurve(
                new Vector2(0.42f, 0f),
                new Vector2(1f, 1f)));

        private static Lazy<BezierCurve> easeOut = new Lazy<BezierCurve>(
            () => new BezierCurve(
                new Vector2(0f, 0f),
                new Vector2(0.58f, 1f)));

        private static Lazy<BezierCurve> easeInOut = new Lazy<BezierCurve>(
            () => new BezierCurve(
                new Vector2(0.42f, 0f),
                new Vector2(0.58f, 1f)));

        public static BezierCurve Ease => ease.Value;

        public static BezierCurve Linear => linear.Value;

        public static BezierCurve EaseIn => easeIn.Value;

        public static BezierCurve EaseOut => easeOut.Value;

        public static BezierCurve EaseInOut => easeInOut.Value;

        public BezierCurve Reverse
        {
            get
            {
                return new BezierCurve(
                    new Vector2(
                        P0.x,
                        1f - P0.y),
                    new Vector2(
                        P1.x,
                        1f - P1.y),
                    new Vector2(
                        P2.x,
                        1f - P2.y),
                    new Vector2(
                        P3.x,
                        1f - P3.y));
            }
        }

        public BezierCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            P0 = p0;
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public BezierCurve(Vector2 p1, Vector2 p2)
        {
            P0 = Vector2.zero;
            P1 = p1;
            P2 = p2;
            P3 = Vector2.one;
        }

        private readonly Vector2 P0;
        private readonly Vector2 P1;
        private readonly Vector2 P2;
        private readonly Vector2 P3;

        public float GetY(float x)
        {
            double? y = GetY((double)x);
            if (y == null)
            {
                throw new System.Exception("Invalid curve");
            }
            return (float)y;
        }

        public override string ToString()
        {
            return $"P0:{P0.ToString()},P1:{P1.ToString()},P2:{P2.ToString()},P3:{P3.ToString()}";
        }

        private double? GetY(double x)
        {
            // Determine t
            double t;
            if (x == P0.x)
            {
                // Handle corner cases explicitly to prevent rounding errors
                t = 0;
            }
            else if (x == P3.x)
            {
                t = 1;
            }
            else
            {
                // Calculate t
                double a = -P0.x + 3 * P1.x - 3 * P2.x + P3.x;
                double b = 3 * P0.x - 6 * P1.x + 3 * P2.x;
                double c = -3 * P0.x + 3 * P1.x;
                double d = P0.x - x;
                double? tTemp = SolveCubic(a, b, c, d);
                if (tTemp == null) return null;
                t = tTemp.Value;
            }

            // Calculate y from t
            return Cubed(1 - t) * P0.y
                + 3 * t * Squared(1 - t) * P1.y
                + 3 * Squared(t) * (1 - t) * P2.y
                + Cubed(t) * P3.y;
        }

        // Solves the equation ax³+bx²+cx+d = 0 for x ϵ ℝ
        // and returns the first result in [0, 1] or null.
        private static double? SolveCubic(double a, double b, double c, double d)
        {
            if (a == 0) return SolveQuadratic(b, c, d);
            if (d == 0) return 0;

            b /= a;
            c /= a;
            d /= a;

            double q = (3.0 * c - Squared(b)) / 9.0;
            double r = (-27.0 * d + b * (9.0 * c - 2.0 * Squared(b))) / 54.0;
            double disc = Cubed(q) + Squared(r);
            double term1 = b / 3.0;

            if (disc > 0)
            {
                double s = r + System.Math.Sqrt(disc);
                s = (s < 0) ? -CubicRoot(-s) : CubicRoot(s);
                double t = r - System.Math.Sqrt(disc);
                t = (t < 0) ? -CubicRoot(-t) : CubicRoot(t);

                double result = -term1 + s + t;
                if (result >= 0 && result <= 1) return result;
            }
            else if (disc == 0)
            {
                double r13 = (r < 0) ? -CubicRoot(-r) : CubicRoot(r);

                double result = -term1 + 2.0 * r13;
                if (result >= 0 && result <= 1) return result;

                result = -(r13 + term1);
                if (result >= 0 && result <= 1) return result;
            }
            else
            {
                q = -q;
                double dum1 = q * q * q;
                dum1 = System.Math.Acos(r / System.Math.Sqrt(dum1));
                double r13 = 2.0 * System.Math.Sqrt(q);

                double result = -term1 + r13 * System.Math.Cos(dum1 / 3.0);
                if (result >= 0 && result <= 1) return result;

                result = -term1 + r13 * System.Math.Cos((dum1 + 2.0 * System.Math.PI) / 3.0);
                if (result >= 0 && result <= 1) return result;

                result = -term1 + r13 * System.Math.Cos((dum1 + 4.0 * System.Math.PI) / 3.0);
                if (result >= 0 && result <= 1) return result;
            }

            return null;
        }

        // Solves the equation ax² + bx + c = 0 for x ϵ ℝ
        // and returns the first result in [0, 1] or null.
        private static double? SolveQuadratic(double a, double b, double c)
        {
            double result = (-b + System.Math.Sqrt(Squared(b) - 4 * a * c)) / (2 * a);
            if (result >= 0 && result <= 1) return result;

            result = (-b - System.Math.Sqrt(Squared(b) - 4 * a * c)) / (2 * a);
            if (result >= 0 && result <= 1) return result;

            return null;
        }

        private static double Squared(double f)
        {
            return f * f;
        }

        private static double Cubed(double f)
        {
            return f * f * f;
        }

        private static double CubicRoot(double f)
        {
            return System.Math.Pow(f, 1.0 / 3.0);
        }
    }
}