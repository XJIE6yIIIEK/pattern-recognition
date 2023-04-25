using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognotion {
	internal class Bone {
		public Angle xRot;
		public Angle yRot;
		public Angle zRot;

		public Bone(byte[] bytes) {
			xRot = new Angle(BitConverter.ToSingle(bytes[0..4], 0));
			yRot = new Angle(BitConverter.ToSingle(bytes[4..8], 0));
			zRot = new Angle(BitConverter.ToSingle(bytes[8..12], 0));
		}

		public Bone(Angle x, Angle y, Angle z) { 
			xRot = x; 
			yRot = y; 
			zRot = z;
		}

		public Bone(Bone copyBone) {
			xRot = copyBone.xRot;
			yRot = copyBone.yRot;
			zRot = copyBone.zRot;
		}

		public static Bone operator+(Bone a, Bone b){
			return new Bone(a.xRot + b.xRot, a.yRot + b.yRot, a.zRot + b.zRot);
		}
	}
}
