using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
	public struct Vector2//沢山作って捨てるタイプと思われるので、構造体にした
				　//参照型ではなく、値型になる
	{
		public double x, y;
		public Vector2(Vector2 v){
			this.x = v.x;
			this.y = v.y;
		}
		public Vector2(double x, double y){
			this.x = x;
			this.y = y;
		}
		public static bool operator ==(Vector2 v, Vector2 w){
			return (v.x == w.x && v.y == w.y);
			//doubleにイコールをつかうのはちょっとこわい
			//距離が十分近い、に直した方がいいかも
		}
		public override bool Equals(object obj){
			return (this.x == ((Vector2)obj).x && this.y == ((Vector2)obj).y);
			//同じく
		}
		public override int GetHashCode(){
			return (x.GetHashCode() + y.GetHashCode());
			//コンパイラに作れと言われた 使わない
		}
		public static bool operator !=(Vector2 v, Vector2 w){
			return (!(v == w));
		}
		public static Vector2 operator+(Vector2 v, Vector2 w){
			return (new Vector2(v.x + w.x, v.y + w.y));
		}
		public static Vector2 operator -(Vector2 v, Vector2 w){
			return (new Vector2(v.x - w.x, v.y - w.y));
		}
		public static Vector2 operator -(Vector2 v){
			return (new Vector2(-v.x, -v.y));
		}
		public static Vector2 operator +(Vector2 v){
			return (new Vector2(v.x,v.y));
		}
		public static Vector2 operator *(Vector2 v, double d){
			return (new Vector2(v.x * d, v.y * d));
		}
		public static Vector2 operator /(Vector2 v, double d){
			return (new Vector2(v.x / d, v.y / d));
		}
		public double distance(Vector2 v){
			return ((this - v).length());
		}
		public double length(){
			return (Math.Sqrt(x * x + y * y));
		}
		public Vector2 unit(){
			double len = length();
			return len == 0 ? this : this / len;
		}
		public double angle(){
			return Math.Atan2(y,x);
		}
		public override string ToString(){//デバッグ用文字列化
			return ("(" + x.ToString() + "," + y.ToString() + ")");
		}
		public static Vector2 parse(string s){//デバッグ用文字列読みとり
			char[] spl={' ',',','(',')'};//
			var inp = s.Split(spl,StringSplitOptions.RemoveEmptyEntries);
			if (inp.Length != 2) return (new Vector2(0, 0));
			return (new Vector2(double.Parse(inp[0]), double.Parse(inp[1])));
		}
		public static bool RectPointHit(Vector2 top, Vector2 bottom, Vector2 point)
		{
			return (top.x < point.x && point.x < bottom.x && top.y < point.y && point.y < bottom.y);
		}
	}
}
