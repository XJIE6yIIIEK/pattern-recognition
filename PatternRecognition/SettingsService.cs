using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognotion {
	internal class SettingsService {
		public Dictionary<string, string> settings;
		private string settingsPath = "config.ini";

		public SettingsService() {
			settings = new Dictionary<string, string>();

			using(var stream = new StreamReader(settingsPath)) {
				while(!stream.EndOfStream) { 
					string[] lineParams = stream.ReadLine().Split('=');
					settings.Add(lineParams[0], lineParams[1]);
				}
			}
		}
	}
}
