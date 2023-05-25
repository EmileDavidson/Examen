# Never nester

Be a never-nester,

Now what is a never-nester? A never-nester never nests their code, ok not never but we have a limit of \~4&#x20;

now there are 2 ways to remove nesting or limit nesting on this page I'll be talking about inversion but I recommend you also check out the[ Extract methods page ](extract-methods.md)(the other method to limit nesting)

### Inversion

inversion is basically reversing the if statements to an early return most editors already have short cut build in for that, in the editor I use (rider) its alt + enter when my pointer is on the if statement&#x20;

> by reversing the if statements to an early return we can skip the else statements and move all code in the else one nest back&#x20;

#### Good Example

```
public void ProcessData(int option)
{
    if (option != 1)
    {
        // Perform operation for other options or unrecognized option
        // ...
        return;
    }

    // Perform operation for option 1

    if (!condition1)
    {
        // Default operation for option 1
        // ...
        return;
    }

    // Perform sub-operation for condition 1

    if (subCondition1)
    {
        // Perform sub-sub-operation for sub-condition 1
        // ...
        return;
    }

    if (subCondition2)
    {
        // Perform sub-sub-operation for sub-condition 2
        // ...
        return;
    }

    // Default sub-sub-operation
    // ...
}

```

#### Bad Example

```
public void ProcessData(int option)
{
    if (option == 1)
    {
        // Perform operation for option 1
        if (condition1)
        {
            // Perform sub-operation for condition 1
            if (subCondition1)
            {
                // Perform sub-sub-operation for sub-condition 1
                // ...
            }
            else if (subCondition2)
            {
                // Perform sub-sub-operation for sub-condition 2
                // ...
            }
            else
            {
                // Default sub-sub-operation
                // ...
            }
        }
        else if (condition2)
        {
            // Perform sub-operation for condition 2
            // ...
        }
        else
        {
            // Default operation for option 1
            // ...
        }
    }
    else if (option == 2)
    {
        // Perform operation for option 2
        // ...
    }
    else if (option == 3)
    {
        // Perform operation for option 3
        // ...
    }
    else
    {
        // Default operation for unrecognized option
        // ...
    }
}
```



Great video from [CodeAesthetic](https://www.youtube.com/@CodeAesthetic) about it

{% embed url="https://www.youtube.com/watch?ab_channel=CodeAesthetic&t=169s&v=CFRhGnuXG-4" %}
