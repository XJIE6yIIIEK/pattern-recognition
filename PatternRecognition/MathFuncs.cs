using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognotion {
	internal unsafe class MathFuncs {
		public static double FastInvSqrt(double number) {
			double xh = 0.5 * number;
			long i = *(long*)&number;
			i = 0x5fe6eb50c7b537a9 - (i >> 1);
			number = *(double*)&i;
			number = number * (1.5 - (xh * number * number));
			number = number * (1.5 - (xh * number * number));
			number = number * (1.5 - (xh * number * number));
			return number;
		}
	}
}
