FORMAT: 1A
HOST: http://polls.apiblueprint.org/

# Surfdude

This is a sample hypertext API for use with the Surfdude library.

## API Home [/]

### View the root actions [GET]

+ Response 200 (application/json)

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

## Registrations Collection [/registrations?{skip,take}]

### View some registrations [GET]

+ Response 200 (application/json)

        {
            "count": 2,
            "items":
            [
                {
                    "id": "42mnb2erj",
                    "status": "Progressing",
                    "links":
                    [
                        {
                            "rel": "item",
                            "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations/42mnb2erj"
                        }
                    ]
                },
                {
                    "id": "34kjb5n2lls",
                    "status": "Progressing",
                    "links":
                    [
                        {
                            "rel": "item",
                            "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations/34kjb5n2lls"
                        }
                    ]
                }
            ],
            "links":
            [
                {
                    "rel": "invite",
                    "description": "Send someone an invitation to register.",
                    "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations",
                    "method": "POST",
                    "form":
                    [
                        { "name": "accountType", "label": "The type of account to register.", "options": [ "seller", "buyer" ] },
                        { "name": "email", "label": "The email addresss of the person.", "isOptional": true },
                        { "name": "whatsApp", "label": "The WhatsApp addresss of the person.", "isOptional": true }
                    ]
                }
            ]
        }

## Registration [/registrations/{id}]

### View a registration [GET]

+ Response 200 (application/json)

        {
            "id": "34kjb5n2lls",
            "status": "Progressing",
            "contactDetails":
            {
                "email": "lukepuplett@hotmail.com"
            },
            "customerServices":
            {
                "processingInstructions": [ ]
            },
            "links":
            [
                {
                    "rel": "cancel",
                    "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations/34kjb5n2lls",
                    "method": "DELETE"
                },
                {
                    "rel": "update-contact-details",
                    "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations/34kjb5n2lls/applicant/contactdetails",
                    "method": "PUT",
                    "form":
                    [
                        { "name": "email", "label": "The email addresss of the person.", "isOptional": true },
                        { "name": "whatsApp", "label": "The WhatsApp addresss of the person.", "isOptional": true }
                    ]
                },
                {
                    "rel": "add-processing-instruction",
                    "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations/34kjb5n2lls/processinginstructions",
                    "method": "POST",
                    "form":
                    [
                        { "name": "message", "label": "The instruction message for customer services agent to read." }
                    ]
                }
            ]
        }

## Registration Contact Details [/registrations/{id}/applicant/contactdetails]

### View the applicant's contact details [GET]

+ Response 200 (application/json)

        {
            "email": "lukepuplett@hotmail.com"
        }

### Change the applicant's contact details [PUT]

+ Response 200 (application/json)

        {
            "email": "lukepuplett@hotmail.com",
            "links":
            [
                {
                    "rel": "registration",
                    "href": "https://private-ac89c-surfdude.apiary-mock.com/registrations/34kjb5n2lls"
                }
            ]
        }
