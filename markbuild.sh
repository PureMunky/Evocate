echo "Time: $(date -Iseconds)" > ./Tete.Web/wwwroot/build.txt

if [ -z "$TRAVIS_BUILD_NUMBER" ]
then
  echo "Build: local" >> ./Tete.Web/wwwroot/build.txt
else
  echo "Build: $TRAVIS_BUILD_NUMBER" >> ./Tete.Web/wwwroot/build.txt
fi


if [ -z "$TRAVIS_BRANCH" ]
then
  echo "Branch: n/a" >> ./Tete.Web/wwwroot/build.txt
else
  echo "Branch: $TRAVIS_BRANCH" >> ./Tete.Web/wwwroot/build.txt
fi
