using lab04;

ILogger logger1 = Logger.Create();
logger1.Log("000000");
logger1.Log("010101");
logger1.Start("A");
logger1.Log("020202");
logger1.Start("B");
logger1.Log("030303");
logger1.Stop();
logger1.Log("040404");

ILogger logger2 = Logger.Create();
logger2.Log("050505");
logger2.Start("C");
logger2.Log("060606");
logger2.Start("D");
logger2.Log("070707");
logger2.Log("080808");
logger2.Stop();
logger2.Log("090909");
logger2.Log("101010");
logger2.Log("111111");
logger2.Stop();

ILogger logger3 = Logger.Create();
logger3.Log("121212");
logger3.Start("E");
logger3.Start("F");
logger3.Stop();
logger3.Stop();