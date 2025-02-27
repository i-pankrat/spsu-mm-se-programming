﻿using NUnit.Framework;
using System.IO;

namespace WeatherIoC.UnitTests
{
	public class WebParserTests
	{
		[Test]
		public void GetDataTest()
		{
			string data = new WebParser().GetData("https://petrathecat.github.io/smth");

			string rightDataBegin = "<html lang=\"ru\"><head>";

			string vitalPart = "https://youtu.be/dQw4w9WgXcQ";

			Assert.IsTrue(data.Contains(rightDataBegin));

			Assert.IsTrue(data.Contains(vitalPart));

			Assert.Pass();
		}
	}
}
