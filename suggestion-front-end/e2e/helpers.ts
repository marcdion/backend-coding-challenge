import { Selector, ClientFunction } from 'testcafe';

export class helpers {
    static ElementWithId = Selector(id => {
        return document.getElementById(id);
    });

    static GetLocation = ClientFunction(() => document.location.href);

    static async Reach(t: TestController): Promise<TestController> {
        return await t.navigateTo(this.ApplicationEndpoint)
    }

    private static ApplicationEndpoint: string = "http://localhost:8080/";
}