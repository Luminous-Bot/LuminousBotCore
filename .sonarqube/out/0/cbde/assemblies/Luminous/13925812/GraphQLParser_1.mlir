// Skipping function Add(none, none), it contains poisonous unsupported syntaxes

func @_Public_Bot.MutationBucket$T$.Build$$() -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :43 :8) {
^entry :
br ^0

^0: // JumpBlock
%0 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :45 :53) // Not a variable of known type: opname
%1 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :45 :27) // $"{{\"operationName\": \"{opname}\"," (InterpolatedStringExpression)
%2 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :46 :27) // $"\"variables\": {{}}, " (InterpolatedStringExpression)
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :45 :27) // Binary expression on unsupported types $"{{\"operationName\": \"{opname}\"," +                             $"\"variables\": {{}}, "
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :47 :52) // Not a variable of known type: opname
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :47 :64) // string (PredefinedType)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :47 :76) // " " (StringLiteralExpression)
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :47 :81) // Not a variable of known type: Mutations
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :47 :64) // string.Join(" ", Mutations) (InvocationExpression)
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :47 :27) // $"\"query\": \"mutation {opname} {{ {string.Join(" ", Mutations)} }}\" }}" (InterpolatedStringExpression)
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :45 :27) // Binary expression on unsupported types $"{{\"operationName\": \"{opname}\"," +                             $"\"variables\": {{}}, " +                             $"\"query\": \"mutation {opname} {{ {string.Join(" ", Mutations)} }}\" }}"
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :49 :19) // Not a variable of known type: query
return %12 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\State\\GraphQLParser.cs" :49 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function GenerateGQLMutation(none, i1, none, none, none, none), it contains poisonous unsupported syntaxes

// Skipping function RecurseMutateVars(none, none), it contains poisonous unsupported syntaxes

// Skipping function genPropList(none), it contains poisonous unsupported syntaxes

// Skipping function GenerateList(none, none), it contains poisonous unsupported syntaxes

// Skipping function GenerateGQLQuery(none, none), it contains poisonous unsupported syntaxes

// Skipping function genProps(none), it contains poisonous unsupported syntaxes

