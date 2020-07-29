// Skipping function ContainsUsedPrefix(none), it contains poisonous unsupported syntaxes

// Skipping function ExecuteAsync(none, none), it contains poisonous unsupported syntaxes

// Skipping function ExecuteCommand(none, none, none, none), it contains poisonous unsupported syntaxes

func @_Public_Bot.CustomCommandService.Commands.HasName$string$(none) -> i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :622 :12) {
^entry (%_name : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :622 :32)
cbde.store %_name, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :622 :32)
br ^0

^0: // BinaryBranchBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :624 :20) // Not a variable of known type: CommandName
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :624 :35) // Not a variable of known type: name
%3 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :624 :20) // comparison of unknown type: CommandName == name
cond_br %3, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :624 :20)

^1: // JumpBlock
%4 = constant 1 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :625 :27) // true
return %4 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :625 :20)

^2: // BinaryBranchBlock
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :626 :25) // Not a variable of known type: Alts
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :626 :39) // Not a variable of known type: name
%7 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :626 :25) // Alts.Contains(name) (InvocationExpression)
cond_br %7, ^3, ^4 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :626 :25)

^3: // JumpBlock
%8 = constant 1 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :627 :27) // true
return %8 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :627 :20)

^4: // JumpBlock
%9 = constant 0 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :629 :27) // false
return %9 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\CommandService.cs" :629 :20)

^5: // ExitBlock
cbde.unreachable

}
// Skipping function ReadCurrentCommands(none), it contains poisonous unsupported syntaxes

// Skipping function GetChannel(none), it contains poisonous unsupported syntaxes

// Skipping function GetUser(none), it contains poisonous unsupported syntaxes

// Skipping function GetRole(none), it contains poisonous unsupported syntaxes

