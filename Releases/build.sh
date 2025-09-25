cp ../Art/icon.png .
cp ../Art/manifest.json .
cp ../README.md .
cp ../CHANGELOG.md .
cp ../CoronerScopophobia/build/bin/Debug/*.dll ./
#Create the config directory if it doesn't already exist and copy the Strings_* files into it.
mkdir -p ./BepInEx/config/EliteMasterEric-Coroner/ && cp ../LanguageData/* ./BepInEx/config/EliteMasterEric-Coroner/

#Compress everything except for this file into a .zip
zip -r ./CoronerScopophobia.zip ./* -x ./build.*

#CAREFUL. Deletes everything in the folder except for the new zip and this file.
find ../Releases/* -not -name 'build.sh' -not -name 'CoronerScopophobia.zip' -delete