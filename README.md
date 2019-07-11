# Surfdude

Because you can surf the best APIs.

A simple little hypermedia client that dynamically drives a REST API. You can use this to test that an API is genuinely RESTful, i.e. it can be driven entirely by links and hypertext transfer, or you can use this in your application to work a backend API instead of a static URL template based client like Refit.

## Surfdude helps you build radically better APIs

#### Designing APIs by writing the desired client code first tends to drive out much nicer APIs

This is because Surfdude emits helpful errors when it can't find the information that it needs in the hypertext to perform the next step. This lets you fix and tweak the API and retry the Surfdude journey until each error is gone and the entire "wave" of API interactions can be surfed successfully.

When I design an API, I tend to use a Surfdude client in a little console app and then work on an API Blueprint using Apiary and run my Surfdude test client against the Apiary mock until it looks and feels right, then I start coding the real API before pointing my Surfdude client at the real one to check it through.

Not only do I end up with a proper RESTful API that's driven dynamically, but using the API from Postman is better since the API is fully self-documenting. There's no need for Swagger or anything else, just as a good website doesn't come with instructions.

You could potentially even replace the code that executes the steps with code that generates the JSON that would make the step work. This would literally let your API client write its own API Blueprint, which can then be used to scaffold code.

Out-of-the-box, Surfdude uses its built-in library for reading and understanding hypertext controls and formulating HTTP request. This will force your APIs to adhere to a particular hypertext document or media-type that I've invented. However, you can replace these parts for some code that works with a HAL derivative or even HTML.

## API UX and Riding Interaction Waves

The premise is that, like a website, a well designed API is centred around the client's needs and problems much like how web and interaction designers build the experiences we all enjoy on the web. Mostly.

For an API this means thinking of the things that a client would like to do with the API, and their journey through the API to get those things done. At each step of the journey there is opportunity to feedback and inform the client of the impact of any changes and to present alternative options and possible next interactions.

## Sample Ride

Surfdude lets you express an interaction with an API as a ride on a wave.

#### Start from the root / resource

Bookmarking a page on the web is risky since the website owner could move things around. So we all tend to shop by going in the front door. This is true of a REST API and it allows the author to move the URLs around and introduce new paths and trial things without breaking existing clients. This is the true power of _real_ REST.

	SurfReport report = await Surf.Wave("https://private-ac89c-surfdude.apiary-mock.com/")
		.FromRoot()
		.Then("registrations")
		.ThenItem(0)
		.ThenSubmit("update-contact-details", new { email = "mike@beans.com" })
		.ThenRead(out Func<ResourceModel> getContactDetails)
		.Then("registration")
		.ThenSubmit("add-processing-instruction", new { message = "The applicant is partially sighted." })
		.GoAsync();

	Console.WriteLine(getContactDetails().Email);

Notice how the protocol methods and resource identifiers are completely absent, reinforcing the notion that URLs cannot themselves be RESTful; a machine cares not for your wonderfully pluralised nouns and semantic structure, just as an online shopper cares not for the URL sitting beneath a button click.

## Surfdude Media-Type

The above works with JSON returned from the API that looks somewhat like this. Where each link contains all the information to make a successful next HTTP request to move the application along.

        {
            "links":
            [
                {
                    "rel": "registrations",
                    "description": "View the registrations progressing and complete.",
                    "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations",
                    "form":
                    [
                        { "name": "skip", "label": "The number of items to skip over.", "isOptional": true },
                        { "name": "take", "label": "The number of items to return.", "isOptional": true },
                    ]
                },
                {
                    "rel": "registration-finder",
                    "description": "Lets an applicant find their registration in the system.",
                    "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations/{status}",
                    "form":
                    [
                        { "name": "status", "options": [ "active", "inactive", "any" ] },
                        { "name": "email" }
                    ]
                }
            ]
        }

## Bring Your Own Media-Type

As Roy Fielding explained, most of the effort in designing a REST API should go into designing the media-type.

The JSON, HTML, XML or YAML or whatever of your API should contain hypermedia controls that contain all the information needed for the client to make use of those controls. For HTML the `<a>` and `<form>` tags come with clear processing instructions for web browser clients to make use of these controls whenever they are encountered.

The upshot of this uniformity is that a nice website needs no further documentation; if the browser cannot work with the HTML then there is a bug. If a website needs a user manual then there is a serious UX issue. The same is true of an API.

Surfdude lets you replace the classes that read and process your hypertext resources and their controls.

## Evolving APIs

Websites change over time and yet customers rarely notice. Their experience remains uninterrupted, the links keep working and adding an item to a shopping cart continues to work, even though the underlying resources may be changing each day.

A great API using a dynamic client affords the same flexibility since interactions are loosely based on relation names.
