func @_Public_Bot.Modules.Handlers.StateHandler.SaveObject$T$$string.T$(none, none) -> () loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :16 :8) {
^entry (%_name : none, %_value : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :16 :41)
cbde.store %_name, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :16 :41)
%1 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :16 :54)
cbde.store %_value, %1 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :16 :54)
br ^0

^0: // BinaryBranchBlock
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :18 :29) // Not a variable of known type: StateFolder
// Entity from another assembly: Path
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :18 :42) // Path.DirectorySeparatorChar (SimpleMemberAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :18 :71) // Not a variable of known type: name
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :18 :26) // $"{StateFolder}{Path.DirectorySeparatorChar}{name}.state" (InterpolatedStringExpression)
// Entity from another assembly: File
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :19 :29) // Not a variable of known type: path
%8 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :19 :17) // File.Exists(path) (InvocationExpression)
%9 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :19 :16) // !File.Exists(path) (LogicalNotExpression)
cond_br %9, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :19 :16)

^1: // SimpleBlock
// Entity from another assembly: File
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :20 :28) // Not a variable of known type: path
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :20 :16) // File.Create(path) (InvocationExpression)
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :20 :16) // File.Create(path).Close() (InvocationExpression)
br ^2

^2: // SimpleBlock
// Entity from another assembly: JsonConvert
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :21 :54) // Not a variable of known type: value
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :21 :26) // JsonConvert.SerializeObject(value) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Logger
%16 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :22 :47) // Not a variable of known type: name
%17 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :22 :25) // $"Saved the object \"{name}\"!" (InterpolatedStringExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Logger
%18 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :22 :58) // Logger.Severity (SimpleMemberAccessExpression)
%19 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :22 :58) // Logger.Severity.Log (SimpleMemberAccessExpression)
%20 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :22 :12) // Logger.Write($"Saved the object \"{name}\"!", Logger.Severity.Log) (InvocationExpression)
// Entity from another assembly: File
%21 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :23 :30) // Not a variable of known type: path
%22 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :23 :36) // Not a variable of known type: json
%23 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :23 :12) // File.WriteAllText(path, json) (InvocationExpression)
br ^3

^3: // ExitBlock
return

}
func @_Public_Bot.Modules.Handlers.StateHandler.LoadObject$T$$string$(none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :25 :8) {
^entry (%_name : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :25 :38)
cbde.store %_name, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :25 :38)
br ^0

^0: // BinaryBranchBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :27 :29) // Not a variable of known type: StateFolder
// Entity from another assembly: Path
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :27 :42) // Path.DirectorySeparatorChar (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :27 :71) // Not a variable of known type: name
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :27 :26) // $"{StateFolder}{Path.DirectorySeparatorChar}{name}.state" (InterpolatedStringExpression)
// Entity from another assembly: File
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :28 :29) // Not a variable of known type: path
%7 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :28 :17) // File.Exists(path) (InvocationExpression)
%8 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :28 :16) // !File.Exists(path) (LogicalNotExpression)
cond_br %8, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :28 :16)

^1: // JumpBlock
// Entity from another assembly: File
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :30 :28) // Not a variable of known type: path
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :30 :16) // File.Create(path) (InvocationExpression)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :30 :16) // File.Create(path).Close() (InvocationExpression)
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :31 :36) // "State object didnt exists.. uhm shit.." (StringLiteralExpression)
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :31 :22) // new Exception("State object didnt exists.. uhm shit..") (ObjectCreationExpression)
cbde.throw %13 :  none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :31 :16)

^2: // BinaryBranchBlock
// Entity from another assembly: File
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :33 :43) // Not a variable of known type: path
%15 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :33 :26) // File.ReadAllText(path) (InvocationExpression)
%17 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :34 :16) // Not a variable of known type: cont
%18 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :34 :24) // "" (StringLiteralExpression)
%19 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :34 :16) // comparison of unknown type: cont == ""
cond_br %19, ^3, ^4 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :34 :16)

^3: // JumpBlock
%20 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :35 :36) // "State object was empty." (StringLiteralExpression)
%21 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :35 :22) // new Exception("State object was empty.") (ObjectCreationExpression)
cbde.throw %21 :  none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :35 :16)

^4: // BinaryBranchBlock
// Entity from another assembly: JsonConvert
%22 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :36 :62) // Not a variable of known type: cont
%23 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :36 :29) // JsonConvert.DeserializeObject<T>(cont) (InvocationExpression)
%25 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :37 :16) // Not a variable of known type: returnObject
%26 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :37 :32) // null (NullLiteralExpression)
%27 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :37 :16) // comparison of unknown type: returnObject == null
cond_br %27, ^5, ^6 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :37 :16)

^5: // JumpBlock
%28 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :38 :36) // "State object was null." (StringLiteralExpression)
%29 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :38 :22) // new Exception("State object was null.") (ObjectCreationExpression)
cbde.throw %29 :  none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :38 :16)

^6: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Logger
%30 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :39 :48) // Not a variable of known type: name
%31 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :39 :25) // $"Loaded the object \"{name}\"!" (InterpolatedStringExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Logger
%32 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :39 :59) // Logger.Severity (SimpleMemberAccessExpression)
%33 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :39 :59) // Logger.Severity.Log (SimpleMemberAccessExpression)
%34 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :39 :12) // Logger.Write($"Loaded the object \"{name}\"!", Logger.Severity.Log) (InvocationExpression)
%35 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :40 :19) // Not a variable of known type: returnObject
return %35 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\StateHandler.cs" :40 :12)

^7: // ExitBlock
cbde.unreachable

}
// Skipping function SaveAsync(none), it contains poisonous unsupported syntaxes

// Skipping function LoadAsync(), it contains poisonous unsupported syntaxes

