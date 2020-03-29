# VSCode setup for Homeseer scripting on Mac
Ideally, this should be a VSCode extension, however for now it's just a poor man's set of files and instructions.

I'm operating under assumption based on [this post](https://forums.homeseer.com/forum/developer-support/scripts-plug-ins-development-and-libraries/hs3-scripting/95208-full-c-script) that HomeSeer still uses [CS-Script](https://github.com/oleg-shilo/cs-script).

1. Install [Mono for Mac](https://www.mono-project.com/docs/getting-started/install/mac/)
1. Install [VSCode for Mac](https://code.visualstudio.com/)
1. Install the oleg-shilo.cs-script extension
1. Copy this repository into ~/HomeSeer/homeseer-vscode
1. Copy HomeSeer dlls into ~/HomeSeer
1. Take a look at ./setup.sh from this repository
  - it sets up public key SSH autorization for your homeseer
  - configures homes