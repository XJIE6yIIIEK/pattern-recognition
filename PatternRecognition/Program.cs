using PatternRecognotion;
using System.Net.Sockets;
using System.Text;

namespace PatternRecognition {
	public static class Program {
		public static void Main(string[] args) {
			SettingsService service = new SettingsService();
			Recognizer recognizer = new Recognizer();

			Server srv = new Server(service, recognizer);
			srv.Run();
			Console.ReadKey();
		}
	}
}