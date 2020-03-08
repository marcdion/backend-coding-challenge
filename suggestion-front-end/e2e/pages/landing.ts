import { Selector } from 'testcafe';

export const QueryTextBox = Selector('input[type="search"]');

export const LatitudeTextBox = Selector('input[name="latitude"]');
export const LongitudeTextBox = Selector('input[name="longitude"]');
export const MaxResultsTextBox = Selector('input[name="max-results"]');

export const RefreshDataButton = Selector('#button-refresh');
export const PredefinedQuery1Button = Selector('#predifined-query-1');
export const PredefinedQuery2Button = Selector('#predifined-query-2');
export const PredefinedQuery3Button = Selector('#predifined-query-3');

export const ApiDocumentationLink = Selector('#api-documentation-link')
export const ResetDataButton = Selector("#button-reset")

export const SuggestionTableItem = Selector('#suggestion-table-item')