using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace PatternRecognotion {
	internal class Mat3 {
		double[,] matrix;

		public double this[int row, int col] {
			get { return matrix[row, col];  }
			set { matrix[row, col] = value; }
		}

		public Mat3() {
			matrix = new double[3, 3] {
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 }
			};
		}

		public static Mat3 CreateXRotMatrix(double angle) {
			Mat3 rotMat = new Mat3();

			rotMat[0, 0] = 1;

			rotMat[1, 1] = Trig.Cos(angle);
			rotMat[1, 2] = -Trig.Sin(angle);

			rotMat[2, 1] = Trig.Sin(angle);
			rotMat[2, 2] = Trig.Cos(angle);

			return rotMat;
		}

		public static Mat3 CreateYRotMatrix(double angle) {
			Mat3 rotMat = new Mat3();

			rotMat[0, 0] = Trig.Cos(angle);
			rotMat[0, 2] = Trig.Sin(angle);

			rotMat[1, 1] = 1;

			rotMat[2, 0] = -Trig.Sin(angle);
			rotMat[2, 2] = Trig.Cos(angle);

			return rotMat;
		}

		public static Mat3 CreateZRotMatrix(double angle) {
			Mat3 rotMat = new Mat3();

			rotMat[0, 0] = Trig.Cos(angle);
			rotMat[0, 1] = -Trig.Sin(angle);

			rotMat[1, 0] = Trig.Sin(angle);
			rotMat[1, 1] = Trig.Cos(angle);

			rotMat[2, 2] = 1;

			return rotMat;
		}

		public static Mat3 operator* (Mat3 matA, Mat3 matB) {
			Mat3 res = new Mat3();

			for(int i = 0; i < 3; i++) {
				for(int j = 0; j < 3; j++) {
					for(int n = 0; n < 3; n++) {
						res[i, j] += matA[i, n] * matB[n, j];
					}
				}
			}

			return res;
		}

		public static Vec3 operator *(Mat3 mat, Vec3 vec) {
			Vec3 res = new Vec3();

			for(int i = 0; i < 3; i++) {
				for(int j = 0; j < 3; j++) {
					res[i] += mat[i, j] * vec[j];
				}
			}

			return res;
		}

		public Mat3 RotationMatrixMultiply(Mat3 multMat) {
			Mat3 res = this * multMat;

			for(int i = 0; i < 3; i++) {
				double sqr = res[i, 0] * res[i, 0] + res[i, 1] * res[i, 1] + res[i, 2] * res[i, 2];
				double invSqrt = MathFuncs.FastInvSqrt(sqr);

				for(int j = 0; j < 3; j++) {
					res[i, j] = res[i, j] * invSqrt;
				}
			}

			return res;
		}

		public string ToString() {
			string str = "\n========\n";
			str += $"{matrix[0, 0]}    {matrix[0, 1]}    {matrix[0, 2]}\n";
			str += $"{matrix[1, 0]}    {matrix[1, 1]}    {matrix[1, 2]}\n";
			str += $"{matrix[2, 0]}    {matrix[2, 1]}    {matrix[2, 2]}\n";
			str += "\n=========\n";

			return str;
		}
	}
}
