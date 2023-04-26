# Audio Pipeline

## Components&#x20;

1. Environment: This includes environmental sounds, such as city sounds, natural environmental sounds (such as bird songs, water streams, rain), machine sounds, weather conditions (such as thunder), etc.&#x20;
2. Interaction: This includes appropriate interaction sounds for tasks such as picking up objects, opening and closing doors, dragging objects
3. Interface UI: This includes appropriate interface sounds such as clicking on buttons, selecting items, notification sounds, etc.

## Software

Most sound comes from the internet and if we need to edit it we use [Audacity](https://www.image-line.com/), if for some reason we made our own audio for something, we use [FL Studio](https://www.image-line.com/)

## Order

1. We start by finding all the audio we want at the given time
2. After we found it we import it all in to unity (if we didnt do that already while finding it) and make sure we give it right naming and put it in the right folders
3. Then when we have all audio we start putting it in the game its important to use the right audio mixer for the audio so we can control things like the volume per category.&#x20;
4. After adding it to the game we test it does it still sound good? an does it work if so we create a pull request&#x20;
5. There is a difference between the requirements of audio and what we will actively validate during the pull request here you can see what is required and what we validate:&#x20;

{% tabs %}
{% tab title="Requirments" %}
* The file size should not be too big (we don't have an exact size for this but just don't make it unnecessary big)&#x20;
* The file should adhere the naming found in the unity engine guide&#x20;
* The file should be put in the right dictionary
* The audio should fit in with the rest of the game style (mostly on feeling but if we are unsure we ask the team for feedback)
* If the audio contains text is the text correct?
{% endtab %}

{% tab title="Validation" %}
* File size
* File naming
* File dictonary position
{% endtab %}
{% endtabs %}

6. if the validation is finished and there where no problems the pull request will be accepted and the branch will be deleted if it wasn't check the feedback and repeat pull request + validation &#x20;



