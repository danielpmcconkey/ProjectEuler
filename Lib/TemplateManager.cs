using System.Text;

namespace EulerProblems.Lib
{
    public static class TemplateManager
    {
		public static void CreateNewProblemFilesFromTemplate(int first, int last)
		{
			StringBuilder template = new StringBuilder();
			template.AppendLine("namespace EulerProblems.Lib.Problems");
			template.AppendLine("{{");
			template.AppendLine("	public class Euler{2} : Euler");
			template.AppendLine("	{{");
			template.AppendLine("		public Euler{2}() : base()");
			template.AppendLine("		{{");
			template.AppendLine("			title = \"{1}\";");
			template.AppendLine("			problemNumber = {0};");
			template.AppendLine("		}}");
			template.AppendLine("		protected override void Run()");
			template.AppendLine("		{{");
			template.AppendLine("			int answer = 0;");
			template.AppendLine("			PrintSolution(answer.ToString());");
			template.AppendLine("			return;");
			template.AppendLine("		}}");
			template.AppendLine("	}}");
			template.AppendLine("}}");

			const string directory = @"E:\ProjectEuler\Lib\Problems";

			for (int i = first; i <= last; i++)
			{
				string classNumber = i.ToString("0000");
				string problemNumber = i.ToString();
				string title = "Template";

				string fileContents = string.Format(template.ToString(), problemNumber, title, classNumber);
				string fileName = string.Format("Euler{0}.cs", classNumber);
				string path = Path.Combine(directory, fileName);

				if (!File.Exists(path))
				{
					File.WriteAllText(path, fileContents);
				}
			}

		}
	}
}
