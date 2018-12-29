﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis;
using MonoDevelop.Ide.TypeSystem;
using System.Linq;
using MonoDevelop.MSBuildEditor.Language;

namespace MonoDevelop.MSBuildEditor.Schema
{
	class RoslynFunctionInfo : FunctionInfo
	{
		readonly IMethodSymbol symbol;

		public RoslynFunctionInfo (IMethodSymbol symbol) : base (symbol.Name, null)
		{
			this.symbol = symbol;
		}

		public override DisplayText Description => new DisplayText (Ambience.GetSummaryMarkup (symbol), true);

		public override string ReturnType => string.Join (" ", FunctionCompletion.ConvertType (symbol.GetReturnType ()).GetTypeDescription ());
		public override FunctionParameterInfo [] Parameters =>
			symbol.Parameters.Select (p => new RoslynFunctionArgumentInfo (p)).ToArray ();
	}

	class RoslynFunctionArgumentInfo : FunctionParameterInfo
	{
		readonly IParameterSymbol symbol;

		public RoslynFunctionArgumentInfo (IParameterSymbol symbol) : base (symbol.Name, null)
		{
			this.symbol = symbol;
		}

		public override DisplayText Description => new DisplayText (Ambience.GetSummaryMarkup (symbol), true);
		public override string Type => string.Join (" ", FunctionCompletion.ConvertType (symbol.Type).GetTypeDescription ());
	}
}