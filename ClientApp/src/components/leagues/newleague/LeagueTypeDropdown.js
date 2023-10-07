import { Dropdown, DropdownItem, DropdownMenu, DropdownToggle } from "reactstrap";
import LeagueService from '../../../services/LeagueService';
import { useState, useEffect } from "react";

const LeagueTypeDropdown = (props) => {

    const [dropdownlist, setDropdownlist] = useState([]);
    const [selectedItem, setSelectedItem] = useState("");
    const [dropdownOpen, setDropdownOpen] = useState(false);
    const [defaultLabel, setdefaultLabel] = useState("League Type");

    const toggle = () => setDropdownOpen((prevState) => !prevState);

    useEffect(() => {
        async function fetchData() {
            const leagueTypes = await LeagueService.GetLeagueTypes();
            const items = [];
            for (var i = 0; i < leagueTypes.length; i++) {
                items.push(<DropdownItem value={[leagueTypes[i].label,leagueTypes[i].value]} onClick={(e) => { selectedChange(e.target.value); }}>{leagueTypes[i].label}</DropdownItem>);
            }
            setDropdownlist(items);
        }
        fetchData();
    }, [""]);

    const selectedChange = (value) => {
        const parsedValue = value.split(",");
        setSelectedItem(parsedValue[1]);
        setdefaultLabel(`League Type: ${parsedValue[0]}`);
        props.selectedChangeCallback(parsedValue[1]);
    }

    return (
        <>
            <Dropdown isOpen={dropdownOpen} toggle={toggle}>
                <DropdownToggle>{defaultLabel}</DropdownToggle>
                <DropdownMenu>
                    {dropdownlist}
                </DropdownMenu>
            </Dropdown>
        </>
    )
}

export default LeagueTypeDropdown;