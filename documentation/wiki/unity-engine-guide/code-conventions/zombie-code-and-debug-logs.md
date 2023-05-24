# Zombie code & Debug logs

### No print statements in develop&#x20;

Simply put in the develop branch there should not be any debug print since over time this can clutter your console and then you place 5 logs so you can see it more easily and then the next time you add even more logs and it's just simply not good for the development process it also takes a heavy hit on the performance \
\
the only exception is error logs or warning logs these are logs that happen when something goes wrong when it shouldn't think about a null error that you handled by doing a null check here you can log debug.log(".. was null but this should not happen"); telling everyone using it that they did something wrong but you handled it.&#x20;

### Remove zombie code

there should not be any zombie code in the develop with zombie code I mean code that is never triggered or commented out since you don't need it anymore&#x20;

I understand it can be nice to see old work for when you need to change something or want to see how it but this is not something we want to see in the develop since over time it can clutter your code a lot where you need to check if something is used or when you need to find something you need to scroll 10 times longer because of all code you are no longer using&#x20;

exceptions to zombie code are helper methods, method extensions or usefull variables / events that other might want to use but you are currently not since you wrote it and know when to trigger them better than anyone else.&#x20;

