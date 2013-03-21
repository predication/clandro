clandro
=======

An interpreted formula engine for .NET Framework 2 (and higher)

The aim is to build it in a modular, pipeline fashion:

* The engine will process a list of input variables and a string-based mathematical expression.

* The engine will initially tokenize the input into syntactic elements

* The tokens will be parsed into a Syntax Tree

* The Syntax Tree will be evaulated

* Finally a result (probably numeric) will be output

Extra flexibility will be built in the following ways:

1.  API "hooks" between each step to allow further customisation.

2.  The Calculation engine can be extended to allow for extra functions
