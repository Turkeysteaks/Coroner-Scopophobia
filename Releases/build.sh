cp ../Art/icon.png .
cp ../Art/manifest.json .
cp ../README.md .
cp ../CHANGELOG.md .
cp ../CoronerSirenHead/build/bin/Debug/*.dll ./
mkdir -p ./BepInEx/config/EliteMasterEric-Coroner/ && cp ../LanguageData/* ./BepInEx/config/EliteMasterEric-Coroner/

zip -r ./CoronerSirenHead.zip ./* -x ./build.*