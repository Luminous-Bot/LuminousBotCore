func @_Public_Bot.ConfigLoader.LoadConfig$$() -> () loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :15 :8) {
^entry :
br ^0

^0: // BinaryBranchBlock
// Entity from another assembly: Directory
%0 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :17 :34) // Not a variable of known type: DataDirectoryPath
%1 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :17 :17) // Directory.Exists(DataDirectoryPath) (InvocationExpression)
%2 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :17 :16) // !Directory.Exists(DataDirectoryPath) (LogicalNotExpression)
cond_br %2, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :17 :16)

^1: // SimpleBlock
// Entity from another assembly: Directory
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :18 :42) // Not a variable of known type: DataDirectoryPath
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :18 :16) // Directory.CreateDirectory(DataDirectoryPath) (InvocationExpression)
br ^2

^2: // BinaryBranchBlock
// Entity from another assembly: File
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :19 :29) // Not a variable of known type: ConfigPath
%6 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :19 :17) // File.Exists(ConfigPath) (InvocationExpression)
%7 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :19 :16) // !File.Exists(ConfigPath) (LogicalNotExpression)
cond_br %7, ^3, ^4 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :19 :16)

^3: // JumpBlock
// Entity from another assembly: File
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :21 :28) // Not a variable of known type: ConfigPath
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :21 :16) // File.Create(ConfigPath) (InvocationExpression)
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :21 :16) // File.Create(ConfigPath).Close() (InvocationExpression)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :22 :36) // "Config didnt exist, we created it and stopped executing" (StringLiteralExpression)
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :22 :22) // new Exception("Config didnt exist, we created it and stopped executing") (ObjectCreationExpression)
cbde.throw %12 :  none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :22 :16)

^4: // BinaryBranchBlock
// Entity from another assembly: File
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :24 :52) // Not a variable of known type: ConfigPath
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :24 :35) // File.ReadAllText(ConfigPath) (InvocationExpression)
%16 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :25 :16) // Not a variable of known type: configContent
%17 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :25 :33) // "" (StringLiteralExpression)
%18 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :25 :16) // comparison of unknown type: configContent == ""
cond_br %18, ^5, ^6 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :25 :16)

^5: // JumpBlock
%19 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :26 :36) // "Content of config was null" (StringLiteralExpression)
%20 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :26 :22) // new Exception("Content of config was null") (ObjectCreationExpression)
cbde.throw %20 :  none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :26 :16)

^6: // SimpleBlock
// Entity from another assembly: JsonConvert
%21 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :27 :106) // Not a variable of known type: configContent
%22 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :27 :48) // JsonConvert.DeserializeObject<Dictionary<string, object>>(configContent) (InvocationExpression)
%24 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :29 :20) // Not a variable of known type: Config
%25 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :29 :27) // "Token" (StringLiteralExpression)
%26 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :29 :20) // Config["Token"] (ElementAccessExpression)
%27 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :29 :20) // Config["Token"].ToString() (InvocationExpression)
%28 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :30 :23) // Not a variable of known type: Config
%29 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :30 :30) // "StateUrl" (StringLiteralExpression)
%30 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :30 :23) // Config["StateUrl"] (ElementAccessExpression)
%31 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\ConfigLoader.cs" :30 :23) // Config["StateUrl"].ToString() (InvocationExpression)
br ^7

^7: // ExitBlock
return

}
