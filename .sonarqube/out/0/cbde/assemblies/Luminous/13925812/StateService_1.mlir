// Skipping function QueryAsync(none), it contains poisonous unsupported syntaxes

// Skipping function MutateAsync(none), it contains poisonous unsupported syntaxes

// Skipping function ExecuteNoReturnAsync(none), it contains poisonous unsupported syntaxes

func @_Public_Bot.StateService.Exists$T$$string$(none) -> i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :85 :8) {
^entry (%_q : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :85 :37)
cbde.store %_q, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :85 :37)
br ^0

^0: // BinaryBranchBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :87 :31) // Not a variable of known type: q
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :87 :22) // Query<T>(q) (InvocationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :88 :16) // Not a variable of known type: res
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :88 :23) // null (NullLiteralExpression)
%6 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :88 :16) // comparison of unknown type: res == null
cond_br %6, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :88 :16)

^1: // JumpBlock
%7 = constant 0 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :89 :23) // false
return %7 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :89 :16)

^2: // JumpBlock
%8 = constant 1 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :90 :19) // true
return %8 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :90 :12)

^3: // ExitBlock
cbde.unreachable

}
func @_Public_Bot.StateService.Exists$string$(none) -> i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :92 :8) {
^entry (%_q : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :92 :34)
cbde.store %_q, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :92 :34)
br ^0

^0: // JumpBlock
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :94 :39) // Not a variable of known type: q
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :94 :22) // Query<ExistBase>(q) (InvocationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :95 :19) // Not a variable of known type: res
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :95 :19) // res.result (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :95 :19) // res.result.Values (SimpleMemberAccessExpression)
%7 = cbde.unknown : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :95 :19) // res.result.Values.First() (InvocationExpression)
return %7 : i1 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\StateService.cs" :95 :12)

^1: // ExitBlock
cbde.unreachable

}
