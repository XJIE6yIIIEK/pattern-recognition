using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognotion {
	class Server {
		int maxMsgLength = 784;
		byte[] tcpPacket;

		SettingsService settingsService;
		Recognizer recognizer;

		public Server(SettingsService settingsService, Recognizer recognizer) {
			tcpPacket = new byte[maxMsgLength];
			this.settingsService = settingsService;
			this.recognizer = recognizer;
		}

		public async void Run() {
			var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			await tcpClient.ConnectAsync(settingsService.settings["ip"], int.Parse(settingsService.settings["port"]));

			while(true) {
				var response = await tcpClient.ReceiveAsync(tcpPacket);

				Bone[] bones = new Bone[]{
					new Bone(tcpPacket[76..88]),
					new Bone(tcpPacket[160..172]),
					new Bone(tcpPacket[172..184]),
					new Bone(tcpPacket[184..196]),
					new Bone(tcpPacket[196..208]),
					new Bone(tcpPacket[508..520]),
					new Bone(tcpPacket[520..532]),
					new Bone(tcpPacket[532..544]),
				};

				recognizer.HandleFrame(bones);
			}
		}
	}
}
