namespace Graphs
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Graph
    {
        private const string INPUT_FOLDER_NAME = "/Input/";
        private const string OUTPUT_FOLDER_NAME = "/Output/";
        private const string ROOT_LEVEL_OF_PROJECT = "../../..";

        private const string INPUT_FILE_NAME_LIST_OF_EDGES = ROOT_LEVEL_OF_PROJECT  + INPUT_FOLDER_NAME + "InputListOfEdges.txt";
        private const string INPUT_FILE_NAME_ADJACENCY_MATRIX = ROOT_LEVEL_OF_PROJECT + INPUT_FOLDER_NAME + "InputAdjacencyMatrix.txt";
        private const string INPUT_FILE_NAME_INCIDENCE_MATRIX = ROOT_LEVEL_OF_PROJECT + INPUT_FOLDER_NAME + "InputIncidenceMatrix.txt";

        private const string OUTPUT_FILE_NAME_LIST_OF_EDGES = ROOT_LEVEL_OF_PROJECT + OUTPUT_FOLDER_NAME + "OutputListOfEdges.txt";
        private const string OUTPUT_FILE_NAME_ADJACENCY_MATRIX = ROOT_LEVEL_OF_PROJECT + OUTPUT_FOLDER_NAME + "OutputAdjacencyMatrix.txt";
        private const string OUTPUT_FILE_NAME_INCIDENCE_MATRIX = ROOT_LEVEL_OF_PROJECT + OUTPUT_FOLDER_NAME + "OutputIncidenceMatrix.txt";

        private int[,] graphMatrix;

        public Graph(int countOfNodes)
        {
            this.CountOfNodes = countOfNodes;
            this.graphMatrix = new int[this.CountOfNodes, this.CountOfNodes];
        }

        public int CountOfNodes { get; private set; }

        public int[,] InitializeMatrix(string typeOfInputMatrix)
        {
            if (typeOfInputMatrix == GlobalConstants.LIST_OF_EDGES)
            {
                int[,] listOfEdges = GenerateListOfEdges();
                this.graphMatrix = ConvertFromListOfEdgesToAdjacencyMatrix(listOfEdges, this.CountOfNodes);
            }
            else if (typeOfInputMatrix == GlobalConstants.ADJACENCY_MATRIX)
            {
                this.graphMatrix = GenerateAdjacencyMatrix(this.CountOfNodes);
            }
            else if (typeOfInputMatrix == GlobalConstants.INCIDENCE_MATRIX)
            {
                int[,] incidenceMatrix = GenerateIncidenceMatrix(this.CountOfNodes);
                this.graphMatrix = ConvertFromIncidenceToAdjacencyMatrix(incidenceMatrix, this.CountOfNodes);
            }

            return this.graphMatrix;
        }

        public static int[,] ConvertFromAdjacencyMatrixToIncidence(int[,] adjacencyMatrix, int countOfNodes)
        {
            int[,] incidenceMatrix = new int[countOfNodes, GetCountOfEdges(adjacencyMatrix)];

            int counterOfCurrentEdge = 0;
            for (int row = 0; row < adjacencyMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < adjacencyMatrix.GetLength(1); col++)
                {
                    if (row < col && adjacencyMatrix[row, col] == 1)
                    {
                        incidenceMatrix[row, counterOfCurrentEdge] = 1;
                        incidenceMatrix[col, counterOfCurrentEdge] = 1;

                        counterOfCurrentEdge++;
                    }
                }
            }

            return incidenceMatrix;
        }

        public static int[,] ConvertFromAdjacencyMatrixToListOfEdges(int[,] adjacencyMatrix)
        {
            int[,] listOfEdges = new int[GetCountOfEdges(adjacencyMatrix), 2];

            int counterOfCurrentEdge = 0;
            for (int row = 0; row < adjacencyMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < adjacencyMatrix.GetLength(1); col++)
                {
                    if (row < col && adjacencyMatrix[row, col] == 1)
                    {
                        listOfEdges[counterOfCurrentEdge, 0] = row + 1;
                        listOfEdges[counterOfCurrentEdge, 1] = col + 1;

                        counterOfCurrentEdge++;
                    }
                }
            }

            return listOfEdges;
        }

        public static int[,] ConvertFromIncidenceToAdjacencyMatrix(int[,] incidenceMatrix, int countOfNodes)
        {
            int[,] adjacencyMatrix = new int[countOfNodes, countOfNodes];

            for (int col = 0; col < incidenceMatrix.GetLength(1); col++)
            {
                List<int> nodes = new List<int>(2);
                for (int row = 0; row < incidenceMatrix.GetLength(0); row++)
                {
                    if (incidenceMatrix[row, col] == 1)
                    {
                        nodes.Add(row);
                    }
                }

                if (nodes.Count == 2)
                {
                    adjacencyMatrix[nodes[0], nodes[1]] = 1;
                    adjacencyMatrix[nodes[1], nodes[0]] = 1;
                }
            }

            return CloneMatrix(adjacencyMatrix);
        }

        public static int[,] ConvertFromListOfEdgesToAdjacencyMatrix(int[,] listOfEdges, int countOfNodes)
        {
            int[,] adjacencyMatrix = new int[countOfNodes, countOfNodes];

            for (int row = 0; row < listOfEdges.GetLength(0); row++)
            {
                int firstNodeAsMatrixIndex = listOfEdges[row, 0] - 1;
                int secondNodeAsMatrixIndex = listOfEdges[row, 1] - 1;

                adjacencyMatrix[firstNodeAsMatrixIndex, secondNodeAsMatrixIndex] = 1;
                adjacencyMatrix[secondNodeAsMatrixIndex, firstNodeAsMatrixIndex] = 1;
            }

            return CloneMatrix(adjacencyMatrix);
        }

        public void SaveInFileMatrixAsIncidence()
        {
            int[,] incidenceMatrix = ConvertFromAdjacencyMatrixToIncidence(this.graphMatrix, this.CountOfNodes);

            string textToSave = GenerateMatrixAsString(incidenceMatrix);
            SaveInFile(OUTPUT_FILE_NAME_INCIDENCE_MATRIX, textToSave);
        }

        public void SaveInFileMatrixAsAdjacency()
        {
            string textToSave = GenerateMatrixAsString(this.graphMatrix);
            SaveInFile(OUTPUT_FILE_NAME_ADJACENCY_MATRIX, textToSave);
        }

        public void SaveInFileListOfEdges()
        {
            int[,] listOfEdges = ConvertFromAdjacencyMatrixToListOfEdges(this.graphMatrix);

            string textToSave = GenerateMatrixAsString(listOfEdges);
            SaveInFile(OUTPUT_FILE_NAME_LIST_OF_EDGES, textToSave);
        }

        private static void SaveInFile(string path, string textToSave)
        {
            using (StreamWriter reader = new StreamWriter(path))
            {
                reader.WriteLine(textToSave);
            }
        }

        private static int GetCountOfEdges(int[,] adjacencyMatrix)
        {
            int countOfEdges = 0;

            for (int row = 0; row < adjacencyMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < adjacencyMatrix.GetLength(1); col++)
                {
                    if (row > col && adjacencyMatrix[row, col] == 1)
                    {
                        countOfEdges++;
                    }
                }
            }

            return countOfEdges;
        }

        private static string GenerateMatrixAsString(int[,] matrix)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    stringBuilder.Append(matrix[row, col] + " ");
                }

                stringBuilder.AppendLine();
            }

            string resultAsString = stringBuilder.ToString().TrimEnd();
            return resultAsString;
        }

        private static int[,] GenerateListOfEdges()
        {
            int countOfAllLines = File.ReadAllLines(INPUT_FILE_NAME_LIST_OF_EDGES).Length;
            int[,] listOfEdges = new int[countOfAllLines, 2];

            using (StreamReader sr = new StreamReader(INPUT_FILE_NAME_LIST_OF_EDGES))
            {
                string line = string.Empty;
                int row = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    int[] connection = line.Split()
                        .Select(int.Parse)
                        .ToArray();

                    listOfEdges[row, 0] = connection[0];
                    listOfEdges[row, 1] = connection[1];

                    row++;
                }
            }

            return CloneMatrix(listOfEdges);
        }

        private static int[,] GenerateAdjacencyMatrix(int countOfNodes)
        {
            int[,] adjacencyMatrix = new int[countOfNodes, countOfNodes];

            using (StreamReader sr = File.OpenText(INPUT_FILE_NAME_ADJACENCY_MATRIX))
            {
                string line = string.Empty;
                int row = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    int[] rowFromFile = line.Split()
                        .Select(int.Parse)
                        .ToArray();

                    for (int col = 0; col < rowFromFile.Length; col++)
                    {
                        int typeConnection = rowFromFile[col];

                        adjacencyMatrix[row, col] = typeConnection;
                    }

                    row++;
                }
            }

            return CloneMatrix(adjacencyMatrix);
        }

        private static int[,] GenerateIncidenceMatrix(int countOfNodes)
        {
            int countOfAllLines = File.ReadAllLines(INPUT_FILE_NAME_INCIDENCE_MATRIX)[0].Length / 2 + 1;
            int[,] incidenceMatrix = new int[countOfNodes, countOfAllLines];

            using (StreamReader sr = File.OpenText(INPUT_FILE_NAME_INCIDENCE_MATRIX))
            {
                string line = string.Empty;
                int row = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    int[] rowFromFile = line.Split()
                        .Select(int.Parse)
                        .ToArray();

                    for (int col = 0; col < rowFromFile.Length; col++)
                    {
                        int typeConnection = rowFromFile[col];

                        incidenceMatrix[row, col] = typeConnection;
                    }

                    row++;
                }
            }

            return CloneMatrix(incidenceMatrix);
        }

        private static int[,] CloneMatrix(int[,] matrix)
        {
            int[,] clonedMatrix = matrix.Clone() as int[,];

            return clonedMatrix;
        }
    }
}
