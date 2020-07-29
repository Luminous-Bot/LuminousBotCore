func @_Public_Bot.Infraction.genId$$() -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :57 :8) {
^entry :
br ^0

^0: // ForInitializerBlock
%0 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :59 :24) // "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789" (StringLiteralExpression)
%2 = constant 20 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :60 :39)
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :60 :34) // char[20] (ArrayType)
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :60 :30) // new char[20] (ArrayCreationExpression)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :61 :25) // new Random() (ObjectCreationExpression)
%8 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :25)
%9 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :21) // i
cbde.store %8, %9 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :21)
br ^1

^1: // BinaryBranchBlock
%10 = cbde.load %9 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :28)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :32) // Not a variable of known type: stringChars
%12 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :32) // stringChars.Length (SimpleMemberAccessExpression)
%13 = cmpi "slt", %10, %12 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :28)
cond_br %13, ^2, ^3 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :28)

^2: // SimpleBlock
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :16) // Not a variable of known type: stringChars
%15 = cbde.load %9 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :28)
%16 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :16) // stringChars[i] (ElementAccessExpression)
%17 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :33) // Not a variable of known type: chars
%18 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :39) // Not a variable of known type: random
%19 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :51) // Not a variable of known type: chars
%20 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :51) // chars.Length (SimpleMemberAccessExpression)
%21 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :39) // random.Next(chars.Length) (InvocationExpression)
%22 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :65 :33) // chars[random.Next(chars.Length)] (ElementAccessExpression)
br ^4

^4: // SimpleBlock
%23 = cbde.load %9 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :52)
%24 = constant 1 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :52)
%25 = addi %23, %24 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :52)
cbde.store %25, %9 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :63 :52)
br ^1

^3: // JumpBlock
%26 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :68 :41) // Not a variable of known type: stringChars
%27 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :68 :30) // new String(stringChars) (ObjectCreationExpression)
%29 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :69 :19) // Not a variable of known type: finalString
return %29 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\Types\\Infraction.cs" :69 :12)

^5: // ExitBlock
cbde.unreachable

}
