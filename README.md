# Atdl4net

#### The Open Source .NET Solution for FIXatdl

Atdl4net is an open source and not-for-profit C# implementation of [FIXatdl](https://www.fixtrading.org/standards/fixatdl/), the FIX Protocol Algorithmic Trading Definition Language standard developed by FIX Protocol Limited (FPL).  Despite little activity in the repository, Atdl4net is nevertheless in active use in a number of commercial buy-side trading systems.

## Features

* Compatible with .NET 3.5 and .NET 4.x
* Displays algo input screens based on the latest industry-standard FIXatdl 1.1 XML schema
* Reads and generates algorithm-specific FIX message content
* Can be integrated into a trading system, using the supplied sample, run as a standalone testing/validation tool.
* Supports the full set of UI widgets defined in FIXatdl 1.1
* Support for message validation and widget state rules (such as show/hide and enable/disable.)
* Supports strategy filtering, customizable settings, and context-specific views (for example Cancel/Replace mode)
* Written in the C# language using standard libraries.
* Source code can be modified to support in-house FIXatdl schema extensions

## Status

This software was developed by the author (Steve Wilkinson) as FIXatdl was being conceived, and was developed in parallel with the Java reference implementation (see below) in the 2010-2011 timeframe.  Since that time, the author ceased to be directly involved with the FIX Protocol and is no longer able to provide support or fixes for the software.  That said, as noted above, the software is still in active use and the recent move to the MIT License should provide the freedom for interested individuals to take the software forward if desired.

## FIX Engine Integration

Please note that Atdl4net is **NOT** a FIX engine for sending and receiving orders over the wire. Rather, Atdl4net draws order entry screens from FIXatdl templates and gets/sets their FIX parameter values.

If you are intending to implement a full-stack trading system with FIX order capability, you will additionally require a FIX engine such as the open-source [QuickFIX/n](http://quickfixn.org/).

## Acknowledgements

Thanks to the following people who helped make Atdl4net happen:

* Scott Atwell, American Century Investments
* [John Shields](https://github.com/johnnyshields)
* Rick Labs, chair of the FIXatdl working group

## License

Atdl4net was originally dual-licensed by its creator but commercial licensing was subsequently abandoned.  The software is now licensed under the MIT License.

FIX Protocol and FIXatdl are trademarks or service marks of FIX Protocol Limited

Looking for a Java implementation of FIXatdl? Check out [atdl4j](https://github.com/atdl4j/atdl4j)
