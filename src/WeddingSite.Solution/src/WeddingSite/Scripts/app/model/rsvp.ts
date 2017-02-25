import {MenuOption} from "./menuOption";

export class Rsvp {
    constructor(
        public Attending: string,
        public IsCamping: boolean,
        public MenuOptions: MenuOption
    ) { }
}