"""
Module with methods to compactify a JSON file. Ran as a program, the content of the file specified
by the first argument is compacted and written into the file specified by the second argument.
"""

import sys

def main() -> None:
    """
    Call compactify_json on arguments, reading from the first argument and writing to the second.
    """
    assert 2 < len(sys.argv)

    json_raw_file = open(sys.argv[1], "r")
    content: str = json_raw_file.read()
    json_raw_file.close()

    result: str = compactify_json(content)

    json_file = open(sys.argv[2], "w")
    json_file.write()
    json_file.close()

def compactify_json(content: str) -> str:
    """
    Compactify JSON content supplied. This means removing all whitespace that is not inside
    strings.
    """
    to_remove = {"\t", "\r", "\n", " ", }
    in_string = False
    result = ""
    for letter in content:
        if letter == "\"":
            in_string = not in_string
        if not in_string and letter in to_remove:
            continue

        result += letter

    return result

if __name__ == "__main__":
    main()