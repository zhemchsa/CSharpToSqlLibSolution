using System;
using CSharpToSqlLib;


namespace TestCSharpToSqlLib {
    class Program {
        static void Main(string[] args) {

            var conn = new Connection();
            conn.Open("AP");
            Console.WriteLine("Open successful");

            var glaCtrl = new GLAccountsController();
            glaCtrl.Connection = conn;

            //create an istance of new SQL insert statement
            var glaccount = new GLAccount() {
                AccountNo = 1000, AccountDescription = "Insert Test Account"
            };
            // pass into Controller GLA
            //glaCtrl.Insert(glaccount);
            //glaCtrl.Update(glaccount);
            glaCtrl.Delete(glaccount);

            //Test
            //var gla100 = glaCtrl.Get(1000);
            //Console.WriteLine($"GLA 1000 : {gla100.AccountNo} | {gla100.AccountDescription}");


           var gla1 = glaCtrl.Get(1000);
            if(gla1 == null) {
                Console.WriteLine("the results is null");
            } else {Console.WriteLine($"GLA 100 : {gla1.AccountNo} | {gla1.AccountDescription}");

            }
           
/*            var glas = glaCtrl.List();

            foreach(var gla in glas) {
                Console.WriteLine($"{gla.AccountNo} | {gla.AccountDescription}");
            }*/

            conn.Close();
        }
    }
}
