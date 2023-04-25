using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognotion {
	enum YSectors { 
		Forward,
		Backward,
		Left,
		Right
	}

	enum ZSectors { 
		Forward,
		Backward,
		Up,
		Down
	}

	class LinkedListItem {
		public LinkedListItem? prev = null;
		public LinkedListItem? next = null;

		public (YSectors, ZSectors) item;
		public DateTime created;

		public LinkedListItem((YSectors, ZSectors) obj) {
			created = DateTime.Now;
			item = obj;
		}
	}

	internal class LinkedList {
		public LinkedListItem? first = null;
		public LinkedListItem? last = null;
		int count = 0;

		public int Count {
			get { return count; }
		}

		public LinkedList() { }

		public void CheckTimespan() {
			var timeBetweenFrames = last.created - first.created;

			if(timeBetweenFrames.TotalMilliseconds > 1500) {
				first = first.next;
				first.prev = null;
				count--;
			}
		}

		public void Add((YSectors, ZSectors) item) {
			LinkedListItem newItem = new LinkedListItem(item);

			if(count == 0) {
				count++;
				first = newItem;
				last = newItem;
				return;
			}

			var (lastY, lastZ) = last.item;
			var (itemY, itemZ) = item;

			if(lastY == itemY && lastZ == itemZ) {
				last.created = DateTime.Now;
				CheckTimespan();
				return;
			}

			newItem.prev = last;
			last.next = newItem;
			last = newItem;

			count++;

			CheckTimespan();
		}

		public void Reset() {
			last = null;
			first = null;
			count = 0;
		}
	}
}
