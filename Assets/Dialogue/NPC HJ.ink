EXTERNAL VerifyQuest()
VAR questCompleted=false
-> start

=== start ===
A long time ago , a couple of skeletons stole my ring. 
It would be nice if I could get it back. 
By chance , have you come across my ring?
   *[Yes]
     ~VerifyQuest()
     {questCompleted:
     ->success
    -else:
    ->failure
    }
   *[No]
   ->noRing

-> END

===noRing===
Come back if you find my ring.
-> END

===success===
Thank you so much!Here's a reward.
->END

===failure===
Looks like you don't have it.Come back if you find my ring.
->END

===postCompletion===
Thanks for helping me earlier.Good luck with your adventure!
->END
