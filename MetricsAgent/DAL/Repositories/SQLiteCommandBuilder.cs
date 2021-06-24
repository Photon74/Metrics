using System.Text;

namespace MetricsAgent.DAL.Repositories
{
    public class SQLiteCommandBuilder<T> where T : class
    {
        StringBuilder CommandString;

        public string CreateCommandString(T item)
        {
            CommandString = new StringBuilder();
            CommandString.Append($"INSERT INTO {item}(value, time) VALUES(@value, @time)");
            //command.Parameters.AddWithValue("@value", item.Value);
            //command.Parameters.AddWithValue("@time", item.Time);

            return CommandString.ToString();
        }
    }
}
