import { helpers } from "../helpers";
import { QueryTextBox, SuggestionTableItem, MaxResultsTextBox, RefreshDataButton, PredefinedQuery1Button, PredefinedQuery2Button, PredefinedQuery3Button, LatitudeTextBox, LongitudeTextBox} from '../pages/landing';

const LandingExpectedUrl = 'http://localhost:8080/';
const APIDocumentationLinkV1 = 'https://localhost:5001/swagger/index.html?urls.primaryName=Suggestion%20API%20-%20Version%201.0';
const APIDocumentationLinkV2 = 'https://localhost:5001/swagger/index.html?urls.primaryName=Suggestion%20API%20-%20Version%202.0';
const GoogleMapExternalUrl = 'https://www.google.com/maps/search/?api=1&query=46.81228,-71.21454'

fixture`General`
.beforeEach(async t => {
    await helpers.Reach(t);
})

//Basic
test('A user should be able to access the landing page', async t => {
    await t.click(helpers.ElementWithId('home-link'))
    .expect(helpers.GetLocation())
    .contains(LandingExpectedUrl);
});

test('A user should be able to access the API documentation V1 page', async t => {
    await t.click(helpers.ElementWithId('navbarDropdownMenuLink'))
    .click(helpers.ElementWithId('set-api-v1'))
    .click(helpers.ElementWithId('api-documentation-link'))
    .expect(helpers.GetLocation())
    .contains(APIDocumentationLinkV1);
});

test('A user should be able to access the API documentation V2 page', async t => {
    await t.click(helpers.ElementWithId('api-documentation-link'))
    .expect(helpers.GetLocation())
    .contains(APIDocumentationLinkV2);
});


fixture`Queries`
.beforeEach(async t => {
    await helpers.Reach(t);
})

//Queries
test('A user should be able to run a simple query', async t => {
    await t.typeText(QueryTextBox, Data.Queries.Base.query)
    .expect(SuggestionTableItem.count)
    .eql(3);
});

test('A user should be refresh max results count', async t => {
    await t.typeText(QueryTextBox, Data.Queries.MaxResultsChange.query)
    .typeText(MaxResultsTextBox, Data.Queries.MaxResultsChange.maxResults, { replace: true })
    .click(RefreshDataButton)
    .expect(SuggestionTableItem.count)
    .eql(4);
});

test('A user should be able to select first predefined query', async t => {
    await t.click(PredefinedQuery1Button)
    .expect(QueryTextBox.value)
    .contains('londo')
    
    .expect(SuggestionTableItem.count)
    .eql(5);
});

test('A user should be able to select second predefined query', async t => {
    await t.click(PredefinedQuery2Button)
    .expect(QueryTextBox.value)
    .contains('Londo')
    
    .expect(LatitudeTextBox.value)
    .contains('43.70011')
    
    .expect(LongitudeTextBox.value)
    .contains('-79.4163')
    
    .expect(SuggestionTableItem.count)
    .eql(5);
});

test('A user should be able to select third predefined query', async t => {
    await t.click(PredefinedQuery3Button)
    .expect(QueryTextBox.value)
    .contains('New')
    
    .expect(LatitudeTextBox.value)
    .contains('40.71427')
    
    .expect(LongitudeTextBox.value)
    .contains('-74.00597')

    .expect(MaxResultsTextBox.value)
    .contains('3')
    
    .expect(SuggestionTableItem.count)
    .eql(3);
});

test('A user should be able to access the external Google Maps', async t => {
    await t.typeText(QueryTextBox, Data.Queries.External.query)
    .click(helpers.ElementWithId('suggestion-item-link-0'))
    .expect(helpers.GetLocation())
    .contains(GoogleMapExternalUrl);
});

export class Data {
    static Queries = {
        Base: {
            query: 'Que',
        },
        MaxResultsChange: {
            query: 'New',
            maxResults: '4'
        },
        External: {
            query: 'Quebec',
        }
    }
}