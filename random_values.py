# Random Values
# -------------
# You can adapt or expand this Python script for even more
# customisation in data, columns and output formats
# than what is possible with the Random Values plug-in.
#
# Generate datasets with random values for testing purposes.
# Random data based on masks, ranges or lists.
# 
# Bas de Reuver - bdr1976@gmail.com (dec 2023)

import csv
import random
import array as arr
import datetime
from dateutil.relativedelta import relativedelta
import pandas as pd
import string

# ---------------------------------------
# constants
# ---------------------------------------
FILE_NAME = "random_values.csv"
TOTAL_LINES = 1000

# random word lists
alpha_list = ("alfa", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel", "india", "juliet", "kilo", "lima", "mike", "november", "oscar", "papa", "quebec", "romeo", "sierra", "tango", "uniform", "victor", "whiskey", "xray", "yankee", "zulu")
#alpha_list = ("anna", "bernard", "cornelis", "dirk", "eduard", "ferdinand", "gerard", "hendrik", "izaak", "johannes", "karel", "lodewijk", "maria", "nico", "otto", "pieter", "quotiënt", "rudolf", "simon", "theodoor", "utrecht", "victor", "willem", "xantippe", "ypsilon", "ijsbrand", "zaandam")
notreal_list = ("blank", "doesnotexist", "dreamedup", "example", "fabricated", "fakery", "fantasized", "fantasy", "feigned", "fictional", "fictitious", "fictive", "forinstance", "imaginary", "imagined", "invented", "madeup", "makebelieve", "mockup", "nonexistent", "notreal", "phoney", "placeholder", "pretended", "simulated", "specimen", "standin", "testcase", "unreal")
#notreal_list = ("bestaatniet", "denkbeeldig", "fabricatie", "fantasie", "fictie", "fictief", "gefingeerd", "imaginair", "maarnietheus", "nietecht", "nonexistent", "onwerkelijk", "testgeval", "uitdeduimgezogen", "verzinsel", "verzonnen", "voorbeeld")
mslug_list = ("ape", "bat", "cat", "dog", "eel", "fox", "gnu", "hen", "imp", "jay", "koi", "lam", "man", "nit", "owl", "pig", "qua", "ram", "sow", "tit", "ure", "vet", "wow", "xor", "yak", "zho")
lorem_list = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.".split(' ')
nowdate = datetime.datetime.now()
nowyear = nowdate.year

# initialise strings for masks, for example '9999XX' for random zipcodes etc.
VARCHAR_MASK_A = "AEIOU"                   # vowels
VARCHAR_MASK_B = "BCDFGHJKLMNPQRSTVWXYZ"   # consonants
VARCHAR_MASK_0 = "0123456789"              # digits
VARCHAR_MASK_F = "0123456789ABCDEF"        # hexdigits
VARCHAR_MASK_S = "!@#$%^&*+-"              # symbols

VARCHAR_MASK_X = VARCHAR_MASK_A + VARCHAR_MASK_B
VARCHAR_MASK_Y = VARCHAR_MASK_A + VARCHAR_MASK_B + VARCHAR_MASK_0
VARCHAR_MASK_Z = VARCHAR_MASK_A + VARCHAR_MASK_B + VARCHAR_MASK_0 + VARCHAR_MASK_S

# password safe characters, avoid confusion between chracters (for example 1 and l, or 0 and O etc.)
VARCHAR_MASK_PW_A = "aeu" # i o removed
VARCHAR_MASK_PW_B = "bcdfhjkmnpqrtvwxy" # g l s z removed
VARCHAR_MASK_PW_0 = "34678" # 0 1 2 5 9 removed

VARCHAR_MASK_PW_X = VARCHAR_MASK_PW_A + VARCHAR_MASK_PW_B
VARCHAR_MASK_PW_Y = VARCHAR_MASK_PW_A + VARCHAR_MASK_PW_B + VARCHAR_MASK_PW_0
VARCHAR_MASK_PW_Z = VARCHAR_MASK_PW_A + VARCHAR_MASK_PW_B + VARCHAR_MASK_PW_0 + VARCHAR_MASK_S

# adjust upper/lower case
# capitals =  0   : lowercase
# capitals =  1   : uppercase
# capitals =  2   : initcap, uppercase after each space
# capitals = -1   : random
def adjust_casing(strval, capitals):
    # all lower case
    if capitals == 0:
        res = strval.lower()
    elif capitals == 1:
        res = strval.upper()
    else:
        # mix or initcap, make entire inpput lower case
        res = ""
        prevspc = True

        # adjust character for character
        for c in strval:
            upper = False

            if capitals == 2:
                # was previous character a space?
                upper = prevspc
                # is current character a space?
                prevspc = (c == " " | c == "." | c == ",")
            else:
                # mixed/random capitalization
                rnd = random.randint(1, 100) # max = exclusive upper bound, so 1..101, not including max, returns values from 1..100
                upper = (rnd <= 25) # 25% uppercase

            # add to result
            if upper:
                c = c.upper()
            res += c

    return res
                    
# Random string value using a character mask
# mask     = "9999XX", "999-999-9999", "#FFFFFF", "ABAB99" etc.
# pwsafe = True   : Only use password safe characters
# capitals =  0   : lowercase
# capitals =  1   : uppercase
# capitals =  2   : initcap, uppercase after each space
# capitals = -1   : random
# scramble = True : scramble all characters
def random_mask_string(mask, pwsafe=False, capitals=-99, scramble=False):
    # variables
    res = ""

    # randomize mask for passwords
    if scramble:
        letters = list(mask)
        random.shuffle(letters)
        mask = ''.join(letters)

    # parse input mask, example 'ababab99' for random password
    for ch in mask:
        CharList = ""
        if ch == 'A':
            CharList = (VARCHAR_MASK_PW_A if pwsafe else VARCHAR_MASK_A)
        elif ch == 'B':
            CharList = (VARCHAR_MASK_PW_B if pwsafe else VARCHAR_MASK_B)
        elif ch == '9':
            CharList = (VARCHAR_MASK_PW_0 if pwsafe else VARCHAR_MASK_0)
        elif ch == 'F':
            CharList = VARCHAR_MASK_F
        elif ch == '@':
            CharList = VARCHAR_MASK_S
        elif ch == 'X':
            CharList = (VARCHAR_MASK_PW_X if pwsafe else VARCHAR_MASK_X)
        elif ch == 'Y':
            CharList = (VARCHAR_MASK_PW_Y if pwsafe else VARCHAR_MASK_Y)
        elif ch == 'Z':
            CharList = (VARCHAR_MASK_PW_Z if pwsafe else VARCHAR_MASK_Z)
        else: # not a valid mask character, just copy it into password
            res += ch

        # is valid
        if CharList != "":
            r_ch = random.choice(CharList)
            res += random.choice(r_ch)

    if -1 <= capitals <= 2:
        res = adjust_casing(res, capitals)

    # return random varchar value
    return res

# Random datetime with a range and format using datetime mask
def random_datetime_range(mask, date_min=None, date_max=None):
    # use current year as default when no start/end datetime given
    if date_min == None:
        date_min = datetime.datetime(nowyear, 1, 1, 0, 0, 0)
    if date_max == None:
        date_max = datetime.datetime(date_min.year+1, 1, 1, 0, 0, 0)

    # calculate delta in seconds-range
    delta = date_max - date_min
    int_delta = (delta.days * 24 * 60 * 60) + delta.seconds

    # pick a random second in seconds-range
    random_second = random.randrange(int_delta)

    # construct datetime and format
    rnddate = date_min + datetime.timedelta(seconds=random_second)

    return rnddate.strftime(mask)

# random lorem ipsum text
def random_lorem_string(c_min, c_max=None):
    # text how long
    howlong = c_min
    if c_max != None:
        howlong = random.randint(c_min, c_max)

    # built random lorem string
    idx = random.randint(0, len(lorem_list)-1)
    retval = ""
    while len(retval) < howlong:
        if len(retval + lorem_list[idx]) > howlong:
            break
        retval += lorem_list[idx] + " "
        idx = (idx + 1) % len(lorem_list)

    return retval.strip()

# ---------------------------------------
# start creating random dataset
# ---------------------------------------

# create an Empty DataFrame object
df = pd.DataFrame()

column_names = ["patient_id", "birth_date", "sex", "bmi", "length", "weight", "postal_code", "followup_date", "glucose_bl", "lab_verified", "e-mail address", "remarks"]
TOTAL_COLUMNS = len(column_names)

print("Generating random dataset with %s columns:" % TOTAL_COLUMNS)

BIRTHDATE_MIN = nowdate - relativedelta(years=65)
BIRTHDATE_MAX = nowdate - relativedelta(years=18)

for col in range(0, TOTAL_COLUMNS):
    print(("%s.." % (col+1)), end = '')
    tmpval = []
    for row in range(0, 100):
        # differnt columns values
        val = None
        # random values per column
        if col == 0:
            val = ''.join(random.choices(string.digits, k=7))
        elif col == 1:
            val = random_datetime_range("%Y-%m-%d", BIRTHDATE_MIN, BIRTHDATE_MAX)
        elif col == 2:
            val = random.choice(["Male", "Female"])
        elif col == 3:
            val = round(random.uniform(18.0, 25.0), 1) # bmi
        elif col == 4:
            val = random.randint(140, 200) # height
        elif col == 5:
            # weight is based on bmi/height, to get semi-realistic height/weight values
            bmi = df.loc[row,:].values[3] # column 3 is bmi
            height = df.loc[row,:].values[4] # column 4 is height
            val = str(int((height / 100.0) * (height / 100.0) * bmi))
        elif col == 6:
            val = random_mask_string("9999XX")
        elif col == 7:
            val = random_datetime_range("%Y-%m-%d %H:%M:%S")
        elif col == 8:
            ## if random.randint(0, 100) >= 5: # to get 5% missing values
            val = "{0:.2f}".format(random.uniform(3.9, 5.6))
        elif col == 9:
            val = random.choice(["Yes", "Yes", "Yes", "No"]) # 75% Yes, 25% No
        elif col == 10:
            # random fictional e-mail address, compiled out of sevaral random values
            dummy = random.randint(1, 100)
            val = random.choice(alpha_list) if dummy >=50 else random.choice(notreal_list)
            if dummy % 3 == 0:
                val = val + (str(random.randint(50, 99)) if dummy >=50 else str(random.randint(nowyear-30, nowyear-5)))
            if dummy % 2 == 0:
                val = str(random.choice(alpha_list)) if dummy >=50 else random.choice(notreal_list) + "." + val
            val = val + "@" + str(random.choice(notreal_list)) + str(random.choice([".com", ".org", ".net", ".at", ".ch", ".cz", ".de", ".dk", ".es", ".eu", ".fi", ".fr", ".gr", ".hr", ".hu", ".is", ".it", ".ie", ".nl", ".no", ".pl", ".pt", ".se", ".sm", ".ro ", ".ua", ".uk"]))
            val = val.lower()
        elif col == 11:
            val = random_lorem_string(50)
        else:
            val = random_lorem_string(10)
            ## other suggestions:
            ## val = "{0:.2f}".format(random.uniform(1.0, 10.0)).replace('.', ',') # 2 decimal places with comma
            ## val = random_mask_string("XXXXYYYYZZZZ9999", True, -1, True) # password strong, random capitals (-1), characters mixed (True)
            ## val = random_mask_string("BABABA99", True, 0) # password easy
            ## val = random.choice(alpha_list) # random word from list
            ## val = str(random.randint(1, 6)) # dice throw
            ## val = random.choice("ABCDEFG") # one random character
            ## val = str(uuid.uuid4()) # random guid v4, requires 'import uuid'
            ## val = ''.join(random.choices(string.ascii_uppercase, k=7)) # 7 random characters, requires 'import string'
            ## val = "{0:,.2f}".format(100.0 * random.uniform(5000.0, 20000.0), 1) # currency value

        # add value to column
        tmpval.append(val)

    # determine column name
    col_name = "unknown"
    if 0 <= col < TOTAL_COLUMNS:
        col_name = column_names[col]

    # add column to dataframe
    df[col_name] = tmpval

# Generating columns iss ready
print("")
print("ready.")

# Observe the result
print(df)

# drop bmi helper column from file
df = df.drop(columns=["bmi"])

# csv write new output
df.to_csv(FILE_NAME, sep='\t', na_rep='', header=True, index=False, encoding='utf-8')
