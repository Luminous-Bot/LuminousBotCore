func @_Public_Bot.UserHandler.CreateUser$ulong$(none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :19 :8) {
^entry (%_Id : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :19 :38)
cbde.store %_Id, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :19 :38)
br ^0

^0: // JumpBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :21 :22) // Not a variable of known type: client
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :21 :37) // Not a variable of known type: Id
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :21 :22) // client.GetUser(Id) (InvocationExpression)
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :22 :29) // Not a variable of known type: usr
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :22 :20) // new User(usr) (ObjectCreationExpression)
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :23 :12) // Not a variable of known type: Users
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :23 :22) // Not a variable of known type: u
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :23 :12) // Users.Add(u) (InvocationExpression)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :24 :19) // Not a variable of known type: u
return %11 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\UserHandler.cs" :24 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function GetUser(none), it contains poisonous unsupported syntaxes

