using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Net.NetworkInformation;

namespace TH1
{
	class BFS
	{
		static string fileInput = "Input.txt";
		static string fileOutput = "Output.txt";
		static StreamWriter sw = new StreamWriter(fileOutput);
		private Dictionary<string, List<string>> graph;
		private string startPoint="",endPoint="";
		BFS(Dictionary<string,List<string>> _graph, string _startPoint,string _endPoint ) 
		{
			graph = _graph;
			startPoint = _startPoint;
			endPoint = _endPoint;
		}

		public List<string> BreathFirstSearchAlgorithm()
		{
			Queue<string> queuePoint = new Queue<string>();
			Dictionary<string,string> parentPoint = new Dictionary<string,string>();
			List<string> visited = new List<string>();
			queuePoint.Enqueue(startPoint);
			visited.Add(startPoint);
			string currentPoint;
			while(queuePoint.Count > 0)
			{
				string step = "";
				string dsL = "";
				string dsQ = "";
				currentPoint=queuePoint.Dequeue();
				if(currentPoint == endPoint)
				{
					//step += currentPoint + "\t|\t" + "STOP";
					step = string.Format("{0,-30}|{1,-20}",currentPoint,"STOP");
					sw.WriteLine(step);
					return Road(parentPoint);
				}
				else if(graph.ContainsKey(currentPoint))
				{
					//step += currentPoint+"\t|\t";
					foreach (string point in graph[currentPoint])
					{
						step += point + " ";
						if (!visited.Contains(point))
						{
							queuePoint.Enqueue(point);
							visited.Add(point);
							parentPoint.Add(point, currentPoint);
						}
							
					}
					//step += "\t|\t";
					foreach(string point in queuePoint)
					{
						dsL+= point + " ";
					}
					foreach(string point in visited)
					{
						dsQ+= point +" ";
					}

					//sw.WriteLine(step);
					sw.Write(string.Format("{0,-30}|{1,-20}|{2,-20}|{3,-20}\n", currentPoint, step, dsL,dsQ));
				}
			}
			return new List<string>();
		}
		public List<string> Road(Dictionary<string,string> _parentPoint)
		{
			string currentPoint = endPoint;
			List<string> _road= new List<string>();
			_road.Add(currentPoint);
			while(currentPoint != startPoint) 
			{
				currentPoint = _parentPoint[currentPoint];
				_road.Add(currentPoint);
			}
			return _road;
		}

		static void Main(string[] args)
		{
			Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
			string startPoint = "";
			string endPoint = "";
			using(StreamReader sr = new StreamReader(fileInput))
			{
				int numEdges = int.Parse(sr.ReadLine());
				string[] line = sr.ReadLine().Split(" ");
				startPoint = line[0];
				endPoint = line[1];
				for (int i = 1; i <= numEdges; i++)
				{
					string[] part = sr.ReadLine().Split(" ");
					if (graph.ContainsKey(part[0]))
					{
						graph[part[0]].Add(part[1]);
					}
					else
					{
						graph.Add(part[0], new List<string>());
						graph[part[0]].Add(part[1]);
					}
				}
			}
			BFS project = new BFS(graph, startPoint, endPoint);
			//List<string> edges = project.BreathFirstSearchAlgorithm();
			//string road=edges[edges.Count-1];
			//for(int i = edges.Count-2; i >=0; i--)
			//{
			//	road += "-->"+edges[i];
			//}
			//sw.Write("Phat trien trang thai\t|\tTrang thai ke\t|\tDanh sach L\n");
			sw.Write(string.Format("{0,-30}|{1,-20}|{2,-20}|{3,-20}\n", "Phát triển trạng thái", "Trạng thái kề", "Danh sách L","Các đỉnh đã xét"));
			List<string> edges = project.BreathFirstSearchAlgorithm();
			string road = edges[edges.Count - 1];
			for (int i = edges.Count - 2; i >= 0; i--)
			{
				road += "-->" + edges[i];
			}
			sw.Write("Duong di tu " + startPoint + " den " + endPoint + " la:\n" + road);
			sw.Dispose();
		}
	}

	class Program
	{
		
	}
}
