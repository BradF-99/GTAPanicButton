# GTAPanicButton

![A screenshot of the program.](https://repository-images.githubusercontent.com/192721589/1cd26e00-984c-11e9-8191-5364e34804e1)

[**Watch a video of the program in action here!**](https://youtu.be/Q3I0QNe0F00)

This program allows you to put yourself in your own GTA:O online lobby by suspending the game process for 10 seconds. This is particularly useful if some *skid hacker* is setting *everyone on fire with their mods* or if you just want some peace and quiet. Your friends will be able to join the session, but other people may join at random due to the match-making algorithm - and of course we can't fix this.

You can also close the game and all of its processes immediately, in case you take a **massive L** and don't want to save your progress, for example - if you are trying to complete a heist without dying or something.

The key shortcuts work in game (tested in windowed and fullscreen mode with Windows 10).

This isn't a cheat / hack / menu etc. as it never injects itself in to the game nor does it modify the game in any way. It simply tells the computer to suspend the threads and then resume them. This program **does not** affect any other players as it only drops you from the session. 

## Things you need

* **[.NET Framework 4.0](https://dotnet.microsoft.com/download/dotnet-framework/net40)** (preferably the latest version of the framework)
* A copy of the game might help also.

## Troubleshooting

* *My game freezes!*  
That's the point. Just wait.  
However if it's frozen for more than 10 seconds try going to Resource Monitor and resuming the task. This shouldn't happen, so if it does please open an issue.

* *The software says it can't find the GTA process even though it's open!*  
Try launching the program in administrator mode. I don't know. It's never happened to me before. Open an issue if this happens.

* *The program won't open!*  
Make sure you have downloaded the latest version of the .NET Framework [which you can download from here.](https://dotnet.microsoft.com/download/dotnet-framework/net40)

* *My anti-virus suite says this program is a virus / malware / trojan etc!*  
Any builds from this Github releases are confirmed virus-free. You can see this with the VirusTotal scans I add to every release. Your anti-virus suite is probably flagging it as it doesn't have a code signature. No need to worry. Just whitelist it and move on. If you are still worried, the code is right here - see for yourself.

If your problem isn't on this list, or if the solution listed doesn't work, please open a GitHub issue.

## Lovely people who helped make this possible

Thank you to the following people!

* [@JordanOcokoljic](https://github.com/JordanOcokoljic) - first tester, assisted with cleaning up my 5 minute hack job!
* [@charlco](https://github.com/charlco) - discovered exceptions that needed to be handled!
* [@Assasindie](https://github.com/Assasindie) - contributed improvements to exception handling!
* Starwobble and Llamasassboat - suggested the key shortcut to instantly quit the game!
* Magnus Johansson Otiel and henon on StackOverflow - contributed a great amount of the codebase!

## Features that I might implement or might not

* *Controller Support* - press the panic button with your Xbox, PlayStation or Mattel Power Glove controller!
* *A nicer interface* - I don't know how to improve on perfection but I can certainly try!
* *Apple HomeKit / Amazon Alexa / Google Home Integration* (not really)

## Contributing

Please see the [Contributions](CONTRIBUTING.md) guidelines for more details.

## Resources I used  

99% of the program came from here.

* https://stackoverflow.com/a/71457
* https://stackoverflow.com/a/15413314

## Legal stuff

This program and its developers are not associated with Rockstar Games or Take-Two Interactive.  
Rockstar Games, Rockstar North, Grand Theft Auto, the GTA Five, and the Rockstar Games R* marks and logos are trademarks and/or registered trademarks of Take-Two Interactive Software, Inc. in the U.S.A. and/or foreign countries.  
The Power Glove is a registered trademark of Mattel, Inc.

The next bit is in caps so it's probably important.

  THERE IS NO WARRANTY FOR THE PROGRAM, TO THE EXTENT PERMITTED BY
APPLICABLE LAW.  EXCEPT WHEN OTHERWISE STATED IN WRITING THE COPYRIGHT
HOLDERS AND/OR OTHER PARTIES PROVIDE THE PROGRAM "AS IS" WITHOUT WARRANTY
OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO,
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.  THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM
IS WITH YOU.  SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF
ALL NECESSARY SERVICING, REPAIR OR CORRECTION.

  IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING
WILL ANY COPYRIGHT HOLDER, OR ANY OTHER PARTY WHO MODIFIES AND/OR CONVEYS
THE PROGRAM AS PERMITTED ABOVE, BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY
GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE
USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF
DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD
PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS),
EVEN IF SUCH HOLDER OR OTHER PARTY HAS BEEN ADVISED OF THE POSSIBILITY OF
SUCH DAMAGES.


Please see the [license](LICENSE) for more details.
