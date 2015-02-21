using System;
using DxFramework;
class test
{
	public static void consoletest()
	{
		while (true)
		{
			Console.WriteLine("input a,b");
			var a= Vector2.parse(Console.ReadLine());
			var b = Vector2.parse(Console.ReadLine());
			Console.WriteLine("a:"+a);
			Console.WriteLine("b:" + b);
			Console.WriteLine("a:" + a);
			Console.WriteLine("a+b:" + (a+b));
			Console.WriteLine("a-b:" + (a-b));
			a /= 0;
			Console.WriteLine("a:" + a);

			Console.WriteLine("-a:" + -a);
			Console.WriteLine("+a:" + (+a));
			Console.WriteLine("alength:" + a.length());
			Console.WriteLine("aangle:" + a.angle());
			Console.WriteLine("aunit:" + a.unit());
			Console.WriteLine("a==b:" + (a==b));


		}
	}
}