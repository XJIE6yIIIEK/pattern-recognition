using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognotion {
	internal class Vec3 {
		double[] vec;

		public double this[int ind] {
			get { return vec[ind]; }
			set { vec[ind] = value; }
		}

		public Vec3() { 
			vec = new double[3] { 0, 0, 0 };
		}

		public Vec3(double[] vec) {
			this.vec = vec;
		}
	}
}
