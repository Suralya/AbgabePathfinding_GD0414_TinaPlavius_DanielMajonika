//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System;

namespace Tastenhacker.Pathfinding.Core
{
	/// <summary>
	/// Represents a single node in our graph
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Vertex<T>
	{
		protected bool Equals(Vertex<T> other)
		{
			return ID == other.ID;
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		protected int degreeIn, degreeOut;
		private static UInt64 idGenerator;

		public T data; // zugriff auf die daten des types
		public bool marked;
		public int Tag;
        public int PathCost;
 
		/// <summary>
		/// Number of incoming and outgoing edges
		/// </summary>
		public int Degree
		{
			get { return degreeIn + degreeOut; }
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((Vertex<T>) obj);
		}
		
		/// <summary>
		/// Number of incoming edges
		/// </summary>
		public int InDegree
		{
			get { return degreeIn; }
		}

		/// <summary>
		/// Number of outgoing edges
		/// </summary>
		public int OutDegree
		{
			get { return degreeOut; }
		}

		/// <summary>
		/// ID of vertex
		/// </summary>
		public ulong ID { get; private set; }

		/// <summary>
		/// Returns the name given to the vertex
		/// </summary>
		public string Name { get; private set; }


		public string GetInitialString()
		{
			return "\"" + Name + "\"";
		}

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Create new extended vertex (to be used internally)
		/// </summary>
		/// <param name="_data"></param>
		/// <param name="name">Name associated with the vertex (generated automatically if null)</param>
		public Vertex(T _data, string name = null)
		{
			degreeIn = 0;
			degreeOut = 0;
			ID = ++idGenerator;
			marked = false;
			data = _data;

			if (name == null)
			{
				Name = "vertex-" + ID;
			}
			else
			{
				Name = name;
			}
		}
	}
}
