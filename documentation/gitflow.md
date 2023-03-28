# Gitflow

Hoe werken jullie met git? Wat voor conventions houden jullie aan voor naamgeving van commits en branches? Waar maken jullie branches voor en wanneer worden deze weer gemerged? Waar staat de final version? en wat doen jullie als er een conflict is? Wie is waar verantwoordelijk voor? Werken jullie misschien met pull requests? Wie keurt deze goed? Welke assets worden gecommit? en wat houden jullie buiten je repo? Hoe gaan jullie om met grote files?

### Git Workflow

* Branch types

### Branch naming

* Format uitleg
* Type
* Korte beschrijving

### Creating a new branch

* Selecteer branch
* Maak nieuwe branch
* Publish nieuwe branch

### Commit changes

* Commit jou veranderingen lokaal
* Push jou veranderingen naar de remote server

### Create pull request

* In github

## Git Workflow

#### Master

De master branch ook wel de Main branch, hier moet je nooit naar committen. Deze branch is alleen voor het eind project bedoeld.

#### Develop

De develop branch, deze branch komt van de main of. Vanaf deze branch worden alle feature branches gemaakt. En worden ook hier weer gemerged door een pull request.

#### Feature

De feature branch. Als je iets willen toevoegen ook iets maken maak je altijd eerst een feature branch. Deze branch komt van de develop branch af en wordt wanneer de feature af is weer gemerged met de develop branch

#### hotfix

Wanneer er een error of bugg is in develop die snel gefixed moet worden kan dit gedaan worden in een hotfix branch. Een hotfix is simpel gezegt een branch die gebruikt wordt om snelle kleine fixes te maken.

## Branch naming

when naming the branch the name is always seperated by a dash if needed

{type}/branch-name

Type

feature: Feature Dit wordt gebruikt wanneer je een nieuwe feature toevoegt. assets: Assets Dit is niet voor code of iets bedoeld. Alleen als je een asset upload. hotfix: Hotfix Als er een fout is die snel opgelost moet worden maak je een hotfix aan. Prototype: Prototype Als je meerdere opties wilt testen maar niet weet welke er gebruikt gaat worden.

#### Examples:

feature#12/flying-demon-enemy hotfix/level-loading-editor

## Creating a new branch

In github desktop selecteer je het project, en zorg dat je op de Develop branch zit

![Afbeelding1](https://user-images.githubusercontent.com/53999981/218985253-2c0a6e2f-824f-4b18-8b7e-45cdafa9faa7.png)

Klik bovenin op de develop branch. Dan opent er een menu hier moet je op New branch drukken.

![Afbeelding2](https://user-images.githubusercontent.com/53999981/218985298-f451a2da-909d-42dc-a0e8-d7d124de501c.png)

Wanneer je op New branch drukt opent er een nieuw menu. Hier type je de naam van je feature. Let op de naming aangegeven in de Branch naming sectie.

En zorg ervoor dat je de develop branch hebt geselecteerd en niet de main branch

![Afbeelding3](https://user-images.githubusercontent.com/53999981/218985301-60225d11-c82d-45cd-84fa-11024f6dca13.png)

Wanneer je op Create branch drukt. maakt hij jou branch aan waar je kan werken. De laatste stap is hem publishen. in het menu boven staat een knop Publish branch. Druk hierop en hij stuurt jou nieuwe branch naar de remote server.

![Afbeelding4](https://user-images.githubusercontent.com/53999981/218985295-24379455-ee64-401e-88aa-2b125b4e4106.png)

## Commit changes

#### Commit veranderingen

heb je veranderingen gemaakt in de Unity scene. En kan het gecommit worden. links onder kan je jou commit een naam geven en een beschrijving. Zorg dat de naam niet te lang is. En als het nodig is kan je meer informatie geven wat je hebt gedaan.

![Afbeelding5](https://user-images.githubusercontent.com/53999981/218985989-1af0fc1d-fb7a-403f-b6fa-1c84760ac92c.png)

#### Push de gecommite veranderingen

Wanneer je klaar bent met werken Push je het werk naar de remote server. In het midden van je scherm staat Push en je kan ook pushen door boven in je menu bar op Push origin te drukken.

![Afbeelding6](https://user-images.githubusercontent.com/53999981/218986078-221507e2-75a8-4ca2-9288-753059a00aff.png) ![Afbeelding7](https://user-images.githubusercontent.com/53999981/218986083-8b551224-7ab3-4ad4-b908-ed9d7071a028.png)

## Create pull request

Een pull request is de aanvraag om jou branch te merge met de Develop branch. Dit doe je in git zelf.

Eventueel kan je de juiste git pagina openen via github desktop, die de pull request button. ![Afbeelding9](https://user-images.githubusercontent.com/53999981/218986310-f0cfe617-32b9-4014-a5b9-612f26178151.png)

Wanneer je op de git site zit ga je naar de Reposetory. Hier selecteer je jou branch en dan staat er in je scherm Pull request. ![Afbeelding10](https://user-images.githubusercontent.com/53999981/218986312-958effd1-9185-4a9a-8852-58d3911ac647.png)

Klik op pull request om het pull request menu te openen. ![Afbeelding11](https://user-images.githubusercontent.com/53999981/218986316-ee5da55d-d371-4b12-b6b0-f3617498bd5f.png)

Let goed op!. Zorg dat je naar de Develop branch gaat een niet naar de main. links bovenin de image zie je dat de base develop is. de compare is de branch die je wilt laten merge.

voeg ook iemand toe die de pull request kan goedkeuren, voeg jezelf toe als Assignees. Zorg dat er een title is en indien de changes wat vaag kunnen zijn en een beschrijving handig.

wanneer je dit hebt gedaan kan je op Create pull request drukken de pull request wordt dan gechecked op de conventions. Hierna wordt hij goedgekeurd of afgekeurd. Indien er pull request zijn is het de bedoeling dat je deze zelf opgelost. Weet je niet hoe dit moet vraag het dan aan een van de developers.

Als er al staat Can’t automatically merge. is de kans groot op een merge conflict. Deze kan je zelf oplossen en als je niet weet hoe kan je het laten oplossen door iemand die wel weet hoe.
