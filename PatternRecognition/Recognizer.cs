using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;

namespace PatternRecognotion {
	enum Pattern { 
		UpDown,
		DownUp,
		LeftRight,
		RightLeft,
		ForwardBackward,
		BackwardForward
	}

	static class PatternDictionaries { 
		public static Dictionary<Pattern, string> patternsName = new Dictionary<Pattern, string>(){
			{ Pattern.UpDown, "Сверху-вниз" },
			{ Pattern.DownUp, "Снизу-вверх" },
			{ Pattern.RightLeft, "Справа-налево" },
			{ Pattern.LeftRight, "Слева-направо" },
			{ Pattern.ForwardBackward, "На себя" },
			{ Pattern.BackwardForward, "От себя" },
		};
	}

	internal class Recognizer {
		LinkedList queue = new LinkedList();

		public Recognizer() { 
			
		}

		void Recognize() {
			if(queue.Count <= 1) {
				return;
			}

			var start = queue.last.prev;
			var end = queue.last;
			Pattern? pattern = null;

			while(start != null) {
				if((end.created - start.created).TotalMilliseconds < 200) {
					start = start.prev;
					continue;
				}

				var (startY, startZ) = start.item;
				var (endY, endZ) = end.item;

				if (
					startY == YSectors.Left && (startZ == ZSectors.Forward || startZ == ZSectors.Backward) &&
					endY == YSectors.Right && (endZ == ZSectors.Forward || endZ == ZSectors.Backward)) {
					pattern = Pattern.LeftRight;
					break;
				} else if(startY == YSectors.Right && (startZ == ZSectors.Forward || startZ == ZSectors.Backward) &&
					endY == YSectors.Left && (endZ == ZSectors.Forward || endZ == ZSectors.Backward)) {
					pattern = Pattern.RightLeft;
					break;
				} else if(startZ == ZSectors.Up && endZ == ZSectors.Down) {
					pattern = Pattern.UpDown;
					break;
				} else if(startZ == ZSectors.Down && endZ == ZSectors.Up) {
					pattern = Pattern.DownUp;
					break;
				}

				start = start.prev;
			}

			if(pattern == null) {
				return;
			}

			IO.WriteLine($"Распознан жест: {PatternDictionaries.patternsName[(Pattern)pattern]}");
			queue.Reset();
		}

		void AddToQueue((YSectors, ZSectors) sectors) {
			queue.Add(sectors);
			Recognize();
		}

		void SectorChoosing(double yRot, double zRot) {
			YSectors ySector;
			ZSectors zSector;

			if(yRot >= -45 && yRot <= 45) {
				ySector = YSectors.Forward;
			} else if(yRot < -45 && yRot >= -135) {
				ySector = YSectors.Left;
			} else if(yRot > 45 && yRot <= 135) {
				ySector = YSectors.Right;
			} else {
				ySector = YSectors.Backward;
			}

			if(zRot >= -45 && zRot <= 45) {
				zSector = ZSectors.Up;
			} else if(zRot < -45 && zRot >= -135) {
				zSector = ZSectors.Backward;
			} else if(zRot > 45 && zRot <= 135) {
				zSector = ZSectors.Forward;
			} else {
				zSector = ZSectors.Down;
			}

			AddToQueue((ySector, zSector));
		}

		Mat3 CreateRotationMatrix(double x, double y, double z) {
			double xRad = Trig.DegreeToRadian(x);
			double yRad = Trig.DegreeToRadian(y);
			double zRad = Trig.DegreeToRadian(z);

			Mat3 matRotX = Mat3.CreateXRotMatrix(xRad);			
			Mat3 matRotY = Mat3.CreateYRotMatrix(yRad);
			Mat3 matRotZ = Mat3.CreateZRotMatrix(zRad);			

			Mat3 matRotXY = matRotX.RotationMatrixMultiply(matRotY);
			Mat3 matRotYZ = matRotXY.RotationMatrixMultiply(matRotZ);

			return matRotYZ;
		}

		public void HandleFrame(Bone[] bones) {
			Vec3 x = new Vec3(new double[3] { 1.0, 0.0, 0.0});

			Bone startBone = bones[0];
			var globalRotation = CreateRotationMatrix(startBone.xRot.Value, startBone.yRot.Value, startBone.zRot.Value);

			for(int i = 1; i < bones.Length; i++) {
				Bone bone = bones[i];
				var localRotation = CreateRotationMatrix(bone.xRot.Value, bone.yRot.Value, bone.zRot.Value);
				globalRotation = globalRotation.RotationMatrixMultiply(localRotation);				
			}

			x = globalRotation * x;

			double pitch;
			double yaw;

			pitch = Math.Sign(x[0]) * Trig.Acos(x[1]);
			yaw = Math.Sign(x[2]) * Trig.Acos(x[0]/Trig.Sin(pitch));

			pitch = Trig.RadianToDegree(pitch);
			yaw = Trig.RadianToDegree(yaw);

			SectorChoosing(yaw, pitch);
		}
	}
}
