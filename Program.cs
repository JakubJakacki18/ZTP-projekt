namespace ZTP
{
	class Program
	{
		static void Main(string[] args)
		{
			bool exit = false;
			do
			{
				Console.WriteLine("Pick scenario:");
				char scenario = Console.ReadKey().KeyChar;
				switch (scenario)
				{
					case '1':
                        
                        break;
						
					default:
						Console.WriteLine("Wrong scenario, pick again");
						break;
				}
			} while (!exit);
		}
	}

}