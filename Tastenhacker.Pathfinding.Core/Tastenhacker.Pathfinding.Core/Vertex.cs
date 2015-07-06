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

		protected int DegreeIn, DegreeOut;
		private static UInt64 _idGenerator;

		public T Data; // zugriff auf die daten des types
		public bool Marked;
		public int Tag;
        public int PathCost;
 
		/// <summary>
		/// Number of incoming and outgoing edges
		/// </summary>
		public int Degree
		{
			get { return DegreeIn + DegreeOut; }
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
			get { return DegreeIn; }
		}

		/// <summary>
		/// Number of outgoing edges
		/// </summary>
		public int OutDegree
		{
			get { return DegreeOut; }
		}

		/// <summary>
		/// ID of Vertex
		/// </summary>
		public ulong ID { get; private set; }

		/// <summary>
		/// Returns the name given to the Vertex
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
		/// Create new extended Vertex (to be used internally)
		/// </summary>
		/// <param name="data"></param>
		/// <param name="name">Name associated with the Vertex (generated automatically if null)</param>
		public Vertex(T data, string name = null)
		{
			DegreeIn = 0;
			DegreeOut = 0;
			ID = ++_idGenerator;
			Marked = false;
			Data = data;

			if (name == null)
			{
				Name = "Vertex-" + ID;
			}
			else
			{
				Name = name;
			}
		}
	}
}
