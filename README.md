# VSCode setup for Homeseer scripting on Mac
Ideally, this should be a VSCode extension, however for now it's just a poor man's set of files and instructions.

I'm operating under assumption based on [this post](https://forums.homeseer.com/forum/developer-support/scripts-plug-ins-development-and-libraries/hs3-scripting/95208-full-c-script) that HomeSeer still uses [CS-Script](https://github.com/oleg-shilo/cs-script).

1. Install [Mono for Mac](https://www.mono-project.com/docs/getting-started/install/mac/)
1. Install [VSCode for Mac](https://code.visualstudio.com/)
1. Install the oleg-shilo.cs-script extension
1. Clone this repository into your ~/HomeSeer/homeseer-vscode
  ```
	mkdir -p ~/Desktop/HomeSeer
	cd ~/Desktop/HomeSeer
	git clone https://github.com/lexakalex/homeseer-vscode.git
  ```
1. Copy HomeSeer dlls into ~/HomeSeer
1. Take a look at ./setup.sh from this repository. I'd recommend that you run commands outside of the script to understand what they do. They can break your SSH setup. Also they use my homeseer IP address, which you'll need to change.
  - it sets up public key SSH autorization for your homeseer - that will be needed top copy files into your homeseer
  - configures cs-script to use HomeSeer API from ~/Desktop/HomeSeer/dlls by default
1. From your homeseer installation copy FTD2XX_NET.dll HSCF.dll HomeSeerAPI.dll Insteon.dll Mail.dll RssToolkit.dll Scheduler.dll into ~/Desktop/HomeSeer/dlls
1. Clone my scripts repository
   ```
   cd ~/Desktop/HomeSeer
 	 git clone https://github.com/lexakalex/homeseer-scripts.git
   ```
1. Remove the scripts that you don't need (most likely all of them) and add your own scripts.
1. Use hs_connect directive in the cs script to define a 'hs' static field that will point at your HomeSeer instance
   // hs_connect 192.168.0.151:10400
