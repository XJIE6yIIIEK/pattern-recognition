using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognotion {
	internal class Angle {
		float val = 0;

		public float Value {
			get { return val; }
			set { val = value; }
		}

		public Angle(float val) {
			this.val = val;
		}

		public static Angle operator -(Angle a) => new Angle(-a.val);

		public static Angle operator +(Angle a, Angle b) {
			float newAngle = a.val + b.val;

			if(newAngle > 180) {
				newAngle -= 360;
			} else if(newAngle < -180) {
				newAngle += 360;
			}

			return new Angle(newAngle);
		}

		public static Angle operator +(Angle a, float b) {
			float newAngle = a.val + b;

			if(newAngle > 180) {
				newAngle -= 360;
			} else if(newAngle < -180) {
				newAngle += 360;
			}

			return new Angle(newAngle);
		}

		public static Angle operator +(Angle a, int b) {
			float newAngle = a.val + b;

			if(newAngle > 180) {
				newAngle -= 360;
			} else if(newAngle < -180) {
				newAngle += 360;
			}

			return new Angle(newAngle);
		}

		public static float operator -(Angle a, Angle b) => a.val - b.val;
		public static float operator -(Angle a, float b) => a.val - b;
		public static float operator -(Angle a, int b) => a.val - b;

		public static bool operator ==(Angle a, Angle b) => a.val == b.val;
		public static bool operator !=(Angle a, Angle b) => a.val != b.val;
		public static bool operator >(Angle a, Angle b) => a.val > b.val;
		public static bool operator <(Angle a, Angle b) => a.val < b.val;
		public static bool operator >=(Angle a, Angle b) => a.val >= b.val;
		public static bool operator <=(Angle a, Angle b) => a.val <= b.val;

		public static bool operator ==(Angle a, float b) => a.val == b;
		public static bool operator !=(Angle a, float b) => a.val != b;
		public static bool operator >(Angle a, float b) => a.val > b;
		public static bool operator <(Angle a, float b) => a.val < b;
		public static bool operator >=(Angle a, float b) => a.val >= b;
		public static bool operator <=(Angle a, float b) => a.val <= b;

		public static bool operator ==(Angle a, int b) => a.val == b;
		public static bool operator !=(Angle a, int b) => a.val != b;
		public static bool operator >(Angle a, int b) => a.val > b;
		public static bool operator <(Angle a, int b) => a.val < b;
		public static bool operator >=(Angle a, int b) => a.val >= b;
		public static bool operator <=(Angle a, int b) => a.val <= b;
	}
}
