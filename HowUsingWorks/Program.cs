

/* HOW using WORKS */

using Microsoft.VisualBasic;
using System.Data.SqlClient;

MyMain();

void MyMain()
{
    using Test myClass = new Test("FromMain");

    ////throw new Exception("Exception.....");
    //Console.WriteLine("State: {0}", myClass.Connection.State);
    //myClass.Print();

    Test myClass = null;
    try
    {
        myClass = new Test("FromMain");
        throw new Exception("Exception.....");
        Console.WriteLine("State: {0}", myClass.Connection.State);
    }
    finally
    {
        if (myClass != null)
        {
            myClass.Dispose();
        }
    }


}

Test CreateTestClass()
{
    using Test result = new Test("FromFunction");
    return result;
}


class Test : IDisposable
{
    public SqlConnection Connection { get; set; }

    public bool IsDisposed { get; private set; } = false;

    public string Name { get; set; }

    public Test(string name)
    {
        Name = name;
        Connection = new SqlConnection();
        // Connection.Open();
    }

    public void Print()
    {
        Console.WriteLine("From Test Class. Name: {1} IsDisposed: {0}", IsDisposed, Name);
    }

    public void Dispose()
    {
        IsDisposed = true;

        if (Connection is not null)
        {
            if (Connection.State is not System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }

            Connection.Dispose();
            Connection = null;
        }

        Console.WriteLine("Disposing....");
    }
}